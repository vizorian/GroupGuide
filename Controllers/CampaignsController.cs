using AutoMapper;
using Group_Guide.Data.Dtos.Campaigns;
using Group_Guide.Data.Entities;
using Group_Guide.Data.Repositories;
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

        public CampaignsController(IMapper mapper, IGamesRepository gamesRepository, ICampaignsRepository campaignsRepository)
        {
            _mapper = mapper;
            _gamesRepository = gamesRepository;
            _campaignsRepository = campaignsRepository;
        }

        [HttpPost]
        public async Task<ActionResult<CampaignDto>> Post(int gameId, CreateCampaignDto campaignDto)
        {
            var game = await _gamesRepository.Get(gameId);
            if (game == null) return NotFound();
            
            var campaign = _mapper.Map<Campaign>(campaignDto);
            campaign.GameId = gameId;

            await _campaignsRepository.InsertAsync(campaign);

            //Created campaign 201
            return Created($"/api/games/{gameId}/campaigns/{campaign.Id}", _mapper.Map<CampaignDto>(campaign));
        }

        [HttpGet]
        public async Task<IEnumerable<CampaignDto>> GetAll(int gameId)
        {
            var game = await _campaignsRepository.GetAllAsync(gameId);

            // Got all campaigns 200
            return game.Select(o => _mapper.Map<CampaignDto>(o));
        }

        [HttpGet("{campaignId}")]
        public async Task<ActionResult<CampaignDto>> Get(int gameId, int campaignId)
        {
            var game = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (game == null) return NotFound();

            // Got campaign by id 200
            return Ok(_mapper.Map<CampaignDto>(game));
        }

        [HttpPut("{campaignId}")]
        public async Task<ActionResult<CampaignDto>> Put(int gameId, int campaignId, UpdateCampaignDto campaignDto)
        {
            var game = await _gamesRepository.Get(gameId);
            if (game == null) return NotFound();

            var oldCampaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (oldCampaign == null)
                return NotFound();

            _mapper.Map(campaignDto, oldCampaign);

            await _campaignsRepository.UpdateAsync(oldCampaign);

            // Updated campaign 200
            return Ok(_mapper.Map<CampaignDto>(oldCampaign));
        }

        [HttpDelete("{campaignId}")]
        public async Task<ActionResult<CampaignDto>> Delete(int gameId, int campaignId)
        {
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            await _campaignsRepository.DeleteAsync(campaign);

            // Deleted campaign 204
            return NoContent();
        }
    }
}
