using Group_Guide.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Data.Repositories
{
    public interface IGamesRepository
    {
        Task<Game> CreateAsync(Game game);
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetAsync(int id);
        Task<Game> UpdateAsync(Game game);
        Task Delete(Game game);
    }

    public class GamesRepository : IGamesRepository
    {
        private readonly GroupGuideContext _groupGuideContext;

        public GamesRepository(GroupGuideContext groupGuideContext)
        {
            _groupGuideContext = groupGuideContext;
        }

        public async Task<Game> CreateAsync(Game game)
        {
            _groupGuideContext.Games.Add(game);
            await _groupGuideContext.SaveChangesAsync();

            return game;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _groupGuideContext.Games.ToListAsync();
        }

        public async Task<Game> GetAsync(int id)
        {
            return await _groupGuideContext.Games.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Game> UpdateAsync(Game game)
        {
            _groupGuideContext.Games.Update(game);
            await _groupGuideContext.SaveChangesAsync();

            return game;
        }

        public async Task Delete(Game game)
        {
            _groupGuideContext.Games.Remove(game);
            await _groupGuideContext.SaveChangesAsync();
        }
    }
}
