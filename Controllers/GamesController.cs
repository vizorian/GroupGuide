using AutoMapper;
using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Games;
using Group_Guide.Data.Entities;
using Group_Guide.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Controllers
{
    /* GAME
    /api/games GET ALL 200
    /api/games/{id} GET 200
    /api/games POST 201
    /api/games/{id} PUT 200
    /api/games/{id} DELETE 200/204
    */

    [ApiController]
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGamesRepository _gamesRepository;
        private readonly IAuthorizationService _authorizationService;

        public GamesController(IMapper mapper, IGamesRepository gamesRepository, IAuthorizationService authorizationService)
        {
            _mapper = mapper;
            _gamesRepository = gamesRepository;
            _authorizationService = authorizationService;
        }

        [HttpPost]
        public async Task<ActionResult<GameDto>> Post(CreateGameDto gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);
            
            await _gamesRepository.Create(game);

            //Created game 201
            return Created($"/api/games/{game.Id}", _mapper.Map<GameDto>(game));
        }

        [HttpGet]
        public async Task<IEnumerable<GameDto>> GetAll()
        {
            // Got all games 200
            return (await _gamesRepository.GetAll()).Select(o => _mapper.Map<GameDto>(o));
        }

        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameDto>> Get(int gameId)
        {
            var game = await _gamesRepository.Get(gameId);
            if (game == null) return NotFound();

            // Got game by id 200
            return Ok(_mapper.Map<GameDto>(game));
        }

        [HttpPut("{gameId}")]
        [Authorize(Roles = GroupGuideUserRoles.Admin)]
        public async Task<ActionResult<GameDto>> Put(int gameId, UpdateGameDto gameDto)
        {
            var game = await _gamesRepository.Get(gameId);
            if (game == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, game, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
                return Forbid();

            _mapper.Map(gameDto, game);

            await _gamesRepository.Put(game);

            // Updated game 200
            return Ok(_mapper.Map<GameDto>(game));
        }

        [HttpDelete("{gameId}")]
        public async Task<ActionResult<GameDto>> Delete(int gameId)
        {
            var game = await _gamesRepository.Get(gameId);
            if (game == null) return NotFound();

            await _gamesRepository.Delete(game);

            // Deleted game 204
            return NoContent();
        }
    }
}
