using Microsoft.EntityFrameworkCore;
using SocialMediaFeed.BLL.Interfaces;
using SocialMediaFeed.DAL;
using SocialMediaFeed.DAL.Entities;

namespace SocialMediaFeed.BLL.Services
{
    public class PostService(SocialMediaFeedContext context) : IPostService
    {
        public Task<List<Post>> Get() => context.Posts
            .Include(p => p.User)
            .ToListAsync();
    }
}
