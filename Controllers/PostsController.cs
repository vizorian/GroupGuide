using AutoMapper;
using Group_Guide.Data.Dtos.Posts;
using Group_Guide.Data.Entities;
using Group_Guide.Data.Repositories;
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
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGamesRepository _gamesRepository;
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly ITopicsRepository _topicsRepository;
        private readonly IPostsRepository _postsRepository;

        public PostsController(IMapper mapper, IGamesRepository gamesRepository, ICampaignsRepository campaignsRepository, ITopicsRepository topicsRepository, IPostsRepository postsRepository)
        {
            _mapper = mapper;
            _gamesRepository = gamesRepository;
            _campaignsRepository = campaignsRepository;
            _topicsRepository = topicsRepository;
            _postsRepository = postsRepository;
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> Post(int gameId, int campaignId, int topicId, CreatePostDto postDto)
        {
            var game = await _gamesRepository.Get(gameId);
            if (game == null) return NotFound();

            // replace with GetAsyncAll?
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var topic = await _topicsRepository.GetAsync(campaignId, topicId);
            if (topic == null) return NotFound();

            var post = _mapper.Map<Post>(postDto);
            post.TopicId = topicId;

            await _postsRepository.InsertAsync(post);

            //Created post 201
            return Created($"/api/games/{gameId}/campaigns/{campaignId}/topics/{topicId}/posts/{post.Id}", _mapper.Map<PostDto>(post));
        }

        [HttpGet]
        public async Task<IEnumerable<PostDto>> GetAll(int topicId)
        {
            // replace with GetAsync?
            var topic = await _postsRepository.GetAllAsync(topicId);

            // Got all topics 200
            return topic.Select(o => _mapper.Map<PostDto>(o));
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<PostDto>> Get(int topicId, int postId)
        {
            var topic = await _postsRepository.GetAsync(topicId, postId);
            if (topic == null) return NotFound();

            // Got post by id 200
            return Ok(_mapper.Map<PostDto>(topic));
        }

        [HttpPut("{postId}")]
        public async Task<ActionResult<PostDto>> Put(int campaignId, int topicId, int postId, UpdatePostDto postDto)
        {
            var topic = await _topicsRepository.GetAsync(campaignId, topicId);
            if (topic == null) return NotFound();

            var oldPost = await _postsRepository.GetAsync(topicId, postId);
            if (oldPost == null)
                return NotFound();

            _mapper.Map(postDto, oldPost);

            await _postsRepository.UpdateAsync(oldPost);

            // Updated post 200
            return Ok(_mapper.Map<PostDto>(oldPost));
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult<PostDto>> Delete(int topicId, int postId)
        {
            var post = await _postsRepository.GetAsync(topicId, postId);
            if (post == null) return NotFound();

            await _postsRepository.DeleteAsync(post);

            // Deleted post 204
            return NoContent();
        }
    }
}
