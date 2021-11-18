using Group_Guide.Data.Dtos.Auth;
using Group_Guide.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Data.Repositories
{
    public interface ICampaignsRepository
    {
        Task CreateAsync(Campaign campaign);
        Task<IEnumerable<Campaign>> GetAllAsync(int gameId);
        Task<Campaign> GetAsync(int gameId, int campaignId);
        Task<List<GroupGuideUser>> GetPlayersAsync(int gameId, int campaignId);
        Task RemovePlayerAsync(int campaignId, GroupGuideUser player);
        Task UpdateAsync(Campaign campaign);
        Task DeleteAsync(Campaign campaign);
    }

    public class CampaignsRepository : ICampaignsRepository
    {
        private readonly GroupGuideContext _groupGuideContext;

        public CampaignsRepository(GroupGuideContext groupGuideContext)
        {
            _groupGuideContext = groupGuideContext;
        }

        public async Task CreateAsync(Campaign campaign)
        {
            _groupGuideContext.Campaigns.Add(campaign);
            await _groupGuideContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Campaign>> GetAllAsync(int gameId)
        {
            return await _groupGuideContext.Campaigns.Where(o => o.GameId == gameId).ToListAsync();
        }

        public async Task<Campaign> GetAsync(int gameId, int campaignId)
        {
            return await _groupGuideContext.Campaigns.FirstOrDefaultAsync(o => o.GameId == gameId && o.Id == campaignId);
        }

        public async Task<List<GroupGuideUser>> GetPlayersAsync(int gameId, int campaignId)
        {
            return await _groupGuideContext.Campaigns.Where(o => o.GameId == gameId && o.Id == campaignId).SelectMany(o => o.Players).ToListAsync();
        }

        public async Task RemovePlayerAsync(int campaignId, GroupGuideUser player)
        {
            var campaignToUpdate = _groupGuideContext.Campaigns.Find(campaignId);
            _groupGuideContext.Entry(campaignToUpdate).Collection("Players").Load();
            campaignToUpdate.Players.Remove(player);
            await _groupGuideContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Campaign campaign)
        {
            _groupGuideContext.Campaigns.Update(campaign);
            await _groupGuideContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Campaign campaign)
        {
            _groupGuideContext.Campaigns.Remove(campaign);
            await _groupGuideContext.SaveChangesAsync();
        }
    }
}
