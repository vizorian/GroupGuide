using Group_Guide.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Data.Repositories
{
    public interface ITopicsRepository
    {
        Task InsertAsync(Topic topic);
        Task<IEnumerable<Topic>> GetAllAsync(int campaignId);
        Task<Topic> GetAsync(int campaignId, int topicId);
        Task UpdateAsync(Topic topic);
        Task DeleteAsync(Topic topic);
    }

    public class TopicsRepository : ITopicsRepository
    {
        private readonly GroupGuideContext _groupGuideContext;

        public TopicsRepository(GroupGuideContext groupGuideContext)
        {
            _groupGuideContext = groupGuideContext;
        }

        public async Task InsertAsync(Topic topic)
        {
            _groupGuideContext.Topics.Add(topic);
            await _groupGuideContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Topic>> GetAllAsync(int campaignId)
        {
            return await _groupGuideContext.Topics.Where(o => o.CampaignId == campaignId).ToListAsync();
        }

        public async Task<Topic> GetAsync(int campaignId, int topicId)
        {
            return await _groupGuideContext.Topics.FirstOrDefaultAsync(o => o.CampaignId == campaignId && o.Id == topicId);
        }

        public async Task UpdateAsync(Topic topic)
        {
            _groupGuideContext.Topics.Update(topic);
            await _groupGuideContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Topic topic)
        {
            _groupGuideContext.Topics.Remove(topic);
            await _groupGuideContext.SaveChangesAsync();
        }
    }
}
