using AutoMapper;
using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using Group_Guide.Data.Dtos.Campaigns;
using Group_Guide.Data.Entities;
using Group_Guide.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Controllers
{
    /* CAMPAIGN
    /api/games/{gameId}/campaigns GET ALL 200
    /api/games/{gameId}/campaigns/{campaignId} GET 200
    /api/games/{gameId}/campaigns POST 201
    /api/games/{gameId}/campaigns/{campaignId} PUT 200
    /api/games/{gameId}/campaigns/{campaignId} DELETE 200/204
    */

    [ApiController]
    [Route("api/games/{gameId}/campaigns")]
    public class CampaignsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGamesRepository _gamesRepository;
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<GroupGuideUser> _userManager;

        public CampaignsController(IMapper mapper,
                                   IGamesRepository gamesRepository,
                                   ICampaignsRepository campaignsRepository,
                                   IAuthorizationService authorizationService,
                                   UserManager<GroupGuideUser> userManager)
        {
            _mapper = mapper;
            _gamesRepository = gamesRepository;
            _campaignsRepository = campaignsRepository;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = GroupGuideUserRoles.User)]
        public async Task<ActionResult<CampaignDto>> Post(int gameId, CreateCampaignDto campaignDto)
        {
            var game = await _gamesRepository.GetAsync(gameId);
            if (game == null) return NotFound();

            var campaign = _mapper.Map<Campaign>(campaignDto);
            campaign.GameId = gameId;
            campaign.UserId = User.FindFirst(CustomClaims.UserId).Value;

            await _campaignsRepository.CreateAsync(campaign);

            //Created campaign 201
            return Created($"/api/games/{gameId}/campaigns/{campaign.Id}", _mapper.Map<CampaignDto>(campaign));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignDto>>> GetAll(int gameId)
        {
            var campaigns = await _campaignsRepository.GetAllAsync(gameId);

            // Got all campaigns 200
            return Ok(campaigns.Select(o => _mapper.Map<CampaignDto>(o)));
        }

        [HttpGet("{campaignId}")]
        public async Task<ActionResult<CampaignDto>> Get(int gameId, int campaignId)
        {
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            // Got campaign by id 200
            return Ok(_mapper.Map<CampaignDto>(campaign));
        }

        [HttpPut("{campaignId}")]
        [Authorize(Roles = GroupGuideUserRoles.User)]
        public async Task<ActionResult<CampaignDto>> Put(int gameId, int campaignId, UpdateCampaignDto campaignDto)
        {
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null)
                return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, campaign, PolicyNames.SameUser);
            if(!authorizationResult.Succeeded)
                return Forbid();

            _mapper.Map(campaignDto, campaign);

            await _campaignsRepository.UpdateAsync(campaign);

            // Updated campaign 200
            return Ok(_mapper.Map<CampaignDto>(campaign));
        }

        [HttpDelete("{campaignId}")]
        [Authorize(Roles = GroupGuideUserRoles.User)]
        public async Task<ActionResult<CampaignDto>> Delete(int gameId, int campaignId)
        {
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, campaign, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
                return Forbid();

            await _campaignsRepository.DeleteAsync(campaign);

            // Deleted campaign 204
            return NoContent();
        }

        [HttpPut("{campaignId}/AddPlayer/{userId}")]
        [Authorize(Roles = GroupGuideUserRoles.User)]
        public async Task<ActionResult<CampaignDto>> AddPlayer(int gameId, int campaignId, UpdateCampaignDto campaignDto, string userId)
        {
            var game = await _gamesRepository.GetAsync(gameId);
            if (game == null)
                return NotFound();
           
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null)
                return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, campaign, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
                return Forbid();

            _mapper.Map(campaignDto, campaign);
            campaign.Players = await _campaignsRepository.GetPlayersAsync(gameId, campaignId);
            var user = await _userManager.FindByIdAsync(userId);
            if (!campaign.Players.Contains(user))
            {
                campaign.Players.Add(await _userManager.FindByIdAsync(userId));
            }
            else
            {
                return BadRequest("Player is already in the campaign.");
            }

            await _campaignsRepository.UpdateAsync(campaign);

            // Updated campaign 200
            return Ok(_mapper.Map<CampaignDto>(campaign));
        }

        [HttpPut("{campaignId}/RemovePlayer/{userId}")]
        [Authorize(Roles = GroupGuideUserRoles.User)]
        public async Task<ActionResult<CampaignDto>> RemovePlayer(int gameId, int campaignId, UpdateCampaignDto campaignDto, string userId)
        {
            var game = await _gamesRepository.GetAsync(gameId);
            if (game == null) return NotFound();

            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null)
                return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, campaign, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
                return Forbid();
            
            await _campaignsRepository.RemovePlayerAsync(campaignId, await _userManager.FindByIdAsync(userId));

            // Updated campaign 200
            return Ok(_mapper.Map<CampaignDto>(campaign));
        }
    }
}
