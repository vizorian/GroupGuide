using AutoMapper;
using Group_Guide.Data.Dtos.Topics;
using Group_Guide.Data.Entities;
using Group_Guide.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Controllers
{
    /* TOPIC
    /api/games/{gameId}/campaigns/{campaignId}  /topics GET ALL 200
    /api/games/{gameId}/campaigns/{campaignId}  /topics/{topicId} GET 200
    /api/games/{gameId}/campaigns/{campaignId}  /topics POST 201
    /api/games/{gameId}/campaigns/{campaignId}  /topics/{topicId} PUT 200
    /api/games/{gameId}/campaigns/{campaignId}  /topics/{topicId} DELETE 200/204
    */

    [ApiController]
    [Route("api/games/{gameId}/campaigns/{campaignId}/topics")]
    public class TopicsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGamesRepository _gamesRepository;
        private readonly ICampaignsRepository _campaignsRepository;
        private readonly ITopicsRepository _topicsRepository;

        public TopicsController(IMapper mapper, IGamesRepository gamesRepository, ICampaignsRepository campaignsRepository, ITopicsRepository topicsRepository)
        {
            _mapper = mapper;
            _gamesRepository = gamesRepository;
            _campaignsRepository = campaignsRepository;
            _topicsRepository = topicsRepository;
        }

        [HttpPost]
        public async Task<ActionResult<TopicDto>> Post(int gameId, int campaignId, CreateTopicDto topicDto)
        {
            var game = await _gamesRepository.GetAsync(gameId);
            if (game == null) return NotFound();

            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var topic = _mapper.Map<Topic>(topicDto);
            topic.CampaignId = campaignId;

            await _topicsRepository.CreateAsync(topic);

            //Created topic 201
            return Created($"/api/games/{gameId}/campaigns/{campaignId}/topics/{topic.Id}", _mapper.Map<TopicDto>(topic));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetAll(int campaignId)
        {
            var topics = await _topicsRepository.GetAllAsync(campaignId);

            // Got all topics 200
            return Ok(topics.Select(o => _mapper.Map<TopicDto>(o)));
        }

        [HttpGet("{topicId}")]
        public async Task<ActionResult<TopicDto>> Get(int campaignId, int topicId)
        {
            var topic = await _topicsRepository.GetAsync(campaignId, topicId);
            if (topic == null) return NotFound();

            // Got topic by id 200
            return Ok(_mapper.Map<TopicDto>(topic));
        }

        [HttpPut("{topicId}")]
        public async Task<ActionResult<TopicDto>> Put(int gameId, int campaignId, int topicId, UpdateTopicDto topicDto)
        {
            var campaign = await _campaignsRepository.GetAsync(gameId, campaignId);
            if (campaign == null) return NotFound();

            var topic = await _topicsRepository.GetAsync(campaignId, topicId);
            if (topic == null)
                return NotFound();

            _mapper.Map(topicDto, topic);

            await _topicsRepository.UpdateAsync(topic);

            // Updated topic 200
            return Ok(_mapper.Map<TopicDto>(topic));
        }

        [HttpDelete("{topicId}")]
        public async Task<ActionResult<TopicDto>> Delete(int campaignId, int topicId)
        {
            var topic = await _topicsRepository.GetAsync(campaignId, topicId);
            if (topic == null) return NotFound();

            await _topicsRepository.DeleteAsync(topic);

            // Deleted topic 204
            return NoContent();
        }
    }
}
