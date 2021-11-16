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
        Task<Game> Create(Game game);
        Task<IEnumerable<Game>> GetAll();
        Task<Game> Get(int id);
        Task<Game> Put(Game game);
        Task Delete(Game game);
    }

    public class GamesRepository : IGamesRepository
    {
        private readonly GroupGuideContext _groupGuideContext;

        public GamesRepository(GroupGuideContext groupGuideContext)
        {
            _groupGuideContext = groupGuideContext;
        }

        public async Task<Game> Create(Game game)
        {
            _groupGuideContext.Games.Add(game);
            await _groupGuideContext.SaveChangesAsync();

            return game;
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await _groupGuideContext.Games.ToListAsync();
        }

        public async Task<Game> Get(int id)
        {
            return await _groupGuideContext.Games.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Game> Put(Game game)
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
