using AutoMapper;
using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using Group_Guide.Data.Dtos.Sessions;
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
    /* SESSION
    /api/games/{gameId}/campaigns/{campaignId}  /sessions GET ALL 200
    /api/games/{gameId}/campaigns/{campaignId}  /sessions/{sessionId} GET 200
    /api/games/{gameId}/campaigns/{campaignId}  /sessions POST 201
    /api/games/{gameId}/campaigns/{campaignId}  /sessions/{sessionId} PUT 200
    /api/games/{gameId}/campaigns/{campaignId}  /sessions/{sessionId} DELETE 200/204
    */

    [ApiController]
    [Route("api/games/{gameId}/campaigns/{campaignId}/sessions")]
    [Authorize(Roles = GroupGuideUserRoles.User)]
    public class SessionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGamesRepository _gamesRepository;
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly ISessionsRepository _sessionsRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<GroupGuideUser> _userManager;

        public SessionsController(IMapper mapper,
                                  IGamesRepository gamesRepository,
                                  ICampaignsRepository campaignsRepository,
                                  ISessionsRepository sessionsRepository,
                                  IAuthorizationService authorizationService,
                                  UserManager<GroupGuideUser> userManager)
        {
            _mapper = mapper;
            _gamesRepository = gamesRepository;
            _campaignsRepository = campaignsRepository;
            _sessionsRepository = sessionsRepository;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<SessionDto>> Post(int gameId, int campaignId, CreateSessionDto sessionDto)
        {
            var game = await _gamesRepository.GetAsync(gameId);
            if (game == null) return NotFound();

            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, campaign, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
                return Forbid();

            var session = _mapper.Map<Session>(sessionDto);
            session.CampaignId = campaignId;
            session.User = await _userManager.GetUserAsync(User);
            session.UserId = await _userManager.GetUserIdAsync(session.User);

            await _sessionsRepository.CreateAsync(session);

            //Created session 201
            return Created($"/api/games/{gameId}/campaigns/{campaignId}/sessions/{session.Id}", _mapper.Map<SessionDto>(session));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetAll(int campaignId)
        {
            // replace with GetAsync?
            var sessions = await _sessionsRepository.GetAllAsync(campaignId);

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, sessions.First().Campaign, PolicyNames.UserBelongs);
            if (!authorizationResult.Succeeded)
                return Forbid();

            // Got all sessions 200
            return Ok(sessions.Select(o => _mapper.Map<SessionDto>(o)));
        }

        [HttpGet("{sessionId}")]
        public async Task<ActionResult<SessionDto>> Get(int campaignId, int sessionId)
        {
            var session = await _sessionsRepository.GetAsync(campaignId, sessionId);
            if (session == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, session.Campaign, PolicyNames.UserBelongs);
            if (!authorizationResult.Succeeded)
                return Forbid();

            // Got session by id 200
            return Ok(_mapper.Map<SessionDto>(session));
        }

        [HttpPut("{sessionId}")]
        public async Task<ActionResult<SessionDto>> Put(int gameId, int campaignId, int sessionId, UpdateSessionDto sessionDto)
        {
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var session = await _sessionsRepository.GetAsync(campaignId, sessionId);
            if (session == null)
                return NotFound();

            // fix this
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, session, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
                return Forbid();

            _mapper.Map(sessionDto, session);

            await _sessionsRepository.UpdateAsync(session);

            // Updated session 200
            return Ok(_mapper.Map<SessionDto>(session));
        }

        [HttpDelete("{sessionId}")]
        public async Task<ActionResult<SessionDto>> Delete(int campaignId, int sessionId)
        {
            var session = await _sessionsRepository.GetAsync(campaignId, sessionId);
            if (session == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, session, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
                return Forbid();

            await _sessionsRepository.DeleteAsync(session);

            // Deleted session 204
            return NoContent();
        }
    }
}
