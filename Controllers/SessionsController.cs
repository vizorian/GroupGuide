using AutoMapper;
using Group_Guide.Data.Dtos.Sessions;
using Group_Guide.Data.Entities;
using Group_Guide.Data.Repositories;
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
    public class SessionsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGamesRepository _gamesRepository;
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly ISessionsRepository _sessionsRepository;

        public SessionsController(IMapper mapper, IGamesRepository gamesRepository, ICampaignsRepository campaignsRepository, ISessionsRepository sessionsRepository)
        {
            _mapper = mapper;
            _gamesRepository = gamesRepository;
            _campaignsRepository = campaignsRepository;
            _sessionsRepository = sessionsRepository;
        }

        [HttpPost]
        public async Task<ActionResult<SessionDto>> Post(int gameId, int campaignId, CreateSessionDto sessionDto)
        {
            var game = await _gamesRepository.Get(gameId);
            if (game == null) return NotFound();

            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var session = _mapper.Map<Session>(sessionDto);
            session.CampaignId = campaignId;

            await _sessionsRepository.InsertAsync(session);

            //Created session 201
            return Created($"/api/games/{gameId}/campaigns/{campaignId}/sessions/{session.Id}", _mapper.Map<SessionDto>(session));
        }

        [HttpGet]
        public async Task<IEnumerable<SessionDto>> GetAll(int campaignId)
        {
            // replace with GetAsync?
            var campaign = await _sessionsRepository.GetAllAsync(campaignId);

            // Got all sessions 200
            return campaign.Select(o => _mapper.Map<SessionDto>(o));
        }

        [HttpGet("{sessionId}")]
        public async Task<ActionResult<SessionDto>> Get(int campaignId, int sessionId)
        {
            var campaign = await _sessionsRepository.GetAsync(campaignId, sessionId);
            if (campaign == null) return NotFound();

            // Got session by id 200
            return Ok(_mapper.Map<SessionDto>(campaign));
        }

        [HttpPut("{sessionId}")]
        public async Task<ActionResult<SessionDto>> Put(int gameId, int campaignId, int sessionId, UpdateSessionDto sessionDto)
        {
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var oldSession = await _sessionsRepository.GetAsync(campaignId, sessionId);
            if (oldSession == null)
                return NotFound();

            _mapper.Map(sessionDto, oldSession);

            await _sessionsRepository.UpdateAsync(oldSession);

            // Updated session 200
            return Ok(_mapper.Map<SessionDto>(oldSession));
        }

        [HttpDelete("{sessionId}")]
        public async Task<ActionResult<SessionDto>> Delete(int campaignId, int sessionId)
        {
            var session = await _sessionsRepository.GetAsync(campaignId, sessionId);
            if (session == null) return NotFound();

            await _sessionsRepository.DeleteAsync(session);

            // Deleted session 204
            return NoContent();
        }
    }
}
