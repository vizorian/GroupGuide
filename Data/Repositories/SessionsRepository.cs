using Group_Guide.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Data.Repositories
{
    public interface ISessionsRepository
    {
        Task CreateAsync(Session session);
        Task<IEnumerable<Session>> GetAllAsync(int campaignId);
        Task<Session> GetAsync(int campaignId, int sessionId);
        Task UpdateAsync(Session session);
        Task DeleteAsync(Session session);
    }

    public class SessionsRepository : ISessionsRepository
    {
        private readonly GroupGuideContext _groupGuideContext;

        public SessionsRepository(GroupGuideContext groupGuideContext)
        {
            _groupGuideContext = groupGuideContext;
        }

        public async Task CreateAsync(Session session)
        {
            _groupGuideContext.Sessions.Add(session);
            await _groupGuideContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Session>> GetAllAsync(int campaignId)
        {
            return await _groupGuideContext.Sessions.Where(o => o.CampaignId == campaignId).ToListAsync();
        }

        public async Task<Session> GetAsync(int campaignId, int sessionId)
        {
            return await _groupGuideContext.Sessions.FirstOrDefaultAsync(o => o.CampaignId == campaignId && o.Id == sessionId);
        }

        public async Task UpdateAsync(Session session)
        {
            _groupGuideContext.Sessions.Update(session);
            await _groupGuideContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Session session)
        {
            _groupGuideContext.Sessions.Remove(session);
            await _groupGuideContext.SaveChangesAsync();
        }
    }
}
