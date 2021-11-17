using Group_Guide.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Guide.Data.Repositories
{
    public interface IPostsRepository
    {
        Task CreateAsync(Post post);
        Task<IEnumerable<Post>> GetAllAsync(int topicId);
        Task<Post> GetAsync(int topicId, int postId);
        Task UpdateAsync(Post post);
        Task DeleteAsync(Post post);
    }

    public class PostsRepository : IPostsRepository
    {
        private readonly GroupGuideContext _groupGuideContext;

        public PostsRepository(GroupGuideContext groupGuideContext)
        {
            _groupGuideContext = groupGuideContext;
        }

        public async Task CreateAsync(Post post)
        {
            _groupGuideContext.Posts.Add(post);
            await _groupGuideContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync(int topicId)
        {
            return await _groupGuideContext.Posts.Where(o => o.TopicId == topicId).ToListAsync();
        }

        public async Task<Post> GetAsync(int topicId, int postId)
        {
            return await _groupGuideContext.Posts.FirstOrDefaultAsync(o => o.TopicId == topicId && o.Id == postId);
        }

        public async Task UpdateAsync(Post post)
        {
            _groupGuideContext.Posts.Update(post);
            await _groupGuideContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Post post)
        {
            _groupGuideContext.Posts.Remove(post);
            await _groupGuideContext.SaveChangesAsync();
        }
    }
}
