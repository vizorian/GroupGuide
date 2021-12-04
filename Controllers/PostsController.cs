using AutoMapper;
using Group_Guide.Auth.Model;
using Group_Guide.Data.Dtos.Auth;
using Group_Guide.Data.Dtos.Posts;
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
    /* POST
    /api/games/{gameId}/campaigns/{campaignId}/topics/{topicId} /posts GET ALL 200
    /api/games/{gameId}/campaigns/{campaignId}/topics/{topicId} /posts/{postId} GET 200
    /api/games/{gameId}/campaigns/{campaignId}/topics/{topicId} /posts POST 201
    /api/games/{gameId}/campaigns/{campaignId}/topics/{topicId} /posts/{postId} PUT 200
    /api/games/{gameId}/campaigns/{campaignId}/topics/{topicId} /posts/{postId} DELETE 200/204
    */

    [ApiController]
    [Route("api/games/{gameId}/campaigns/{campaignId}/topics/{topicId}/posts")]
    [Authorize(Roles = GroupGuideUserRoles.User)]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGamesRepository _gamesRepository;
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly ITopicsRepository _topicsRepository;
        private readonly IPostsRepository _postsRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<GroupGuideUser> _userManager;

        public PostsController(IMapper mapper,
                               IGamesRepository gamesRepository,
                               ICampaignsRepository campaignsRepository,
                               ITopicsRepository topicsRepository,
                               IPostsRepository postsRepository,
                               IAuthorizationService authorizationService,
                               UserManager<GroupGuideUser> userManager)
        {
            _mapper = mapper;
            _gamesRepository = gamesRepository;
            _campaignsRepository = campaignsRepository;
            _topicsRepository = topicsRepository;
            _postsRepository = postsRepository;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> Post(int gameId, int campaignId, int topicId, CreatePostDto postDto)
        {
            var game = await _gamesRepository.GetAsync(gameId);
            if (game == null) return NotFound();

            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var topic = await _topicsRepository.GetAsync(campaignId, topicId);
            if (topic == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, campaign, PolicyNames.UserBelongs);
            if (!authorizationResult.Succeeded)
                return Forbid();

            var post = _mapper.Map<Post>(postDto);
            post.TopicId = topicId;
            post.UserId = User.FindFirst(CustomClaims.UserId).Value;

            await _postsRepository.CreateAsync(post);

            //Created post 201
            return Created($"/api/games/{gameId}/campaigns/{campaignId}/topics/{topicId}/posts/{post.Id}", _mapper.Map<PostDto>(post));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll(int gameId, int campaignId, int topicId)
        {
            var posts = await _postsRepository.GetAllAsync(topicId);

            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null)
                return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, campaign, PolicyNames.UserBelongs);
            if (!authorizationResult.Succeeded)
                return Forbid();

            // Got all topics 200
            return Ok(posts.Select(o => _mapper.Map<PostDto>(o)));
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<PostDto>> Get(int gameId, int campaignId, int topicId, int postId)
        {
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var topic = await _topicsRepository.GetAsync(campaignId, topicId);
            if (topic == null) return NotFound();

            var post = await _postsRepository.GetAsync(topicId, postId);
            if (post == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, campaign, PolicyNames.UserBelongs);
            if (!authorizationResult.Succeeded)
                return Forbid();

            // Got post by id 200
            return Ok(_mapper.Map<PostDto>(post));
        }

        [HttpPut("{postId}")]
        public async Task<ActionResult<PostDto>> Put(int gameId, int campaignId, int topicId, int postId, UpdatePostDto postDto)
        {
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var topic = await _topicsRepository.GetAsync(campaignId, topicId);
            if (topic == null) return NotFound();

            var post = await _postsRepository.GetAsync(topicId, postId);
            if (post == null)
                return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, campaign, PolicyNames.UserBelongs);
            if (!authorizationResult.Succeeded)
                return Forbid();

            authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _mapper.Map(postDto, post);

            await _postsRepository.UpdateAsync(post);

            // Updated post 200
            return Ok(_mapper.Map<PostDto>(post));
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult<PostDto>> Delete(int gameId, int campaignId, int topicId, int postId)
        {
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var topic = await _topicsRepository.GetAsync(campaignId, topicId);
            if (topic == null) return NotFound();

            var post = await _postsRepository.GetAsync(topicId, postId);
            if (post == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, campaign, PolicyNames.UserBelongs);
            if (!authorizationResult.Succeeded)
                return Forbid();

            authorizationResult = await _authorizationService.AuthorizeAsync(User, post, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                var authorizationResult2 = await _authorizationService.AuthorizeAsync(User, campaign, PolicyNames.SameUser);
                if (!authorizationResult2.Succeeded)
                    return Forbid();
            }

            await _postsRepository.DeleteAsync(post);

            // Deleted post 204
            return NoContent();
        }
    }
}
