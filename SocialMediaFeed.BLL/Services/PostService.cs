using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaFeed.BLL.Interfaces;
using SocialMediaFeed.BLL.Models;
using SocialMediaFeed.DAL;
using SocialMediaFeed.DAL.Entities;

namespace SocialMediaFeed.BLL.Services
{
    public class PostService(SocialMediaFeedContext context, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, IMapper mapper) : IPostService
    {
        private string UserId => userManager.GetUserId(httpContextAccessor.HttpContext.User)!;

        public async Task<List<Post>> Get()
        {
            var result = await context.Posts
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            result.ForEach(p => p.CanDelete = p.UserId == UserId);

            return result;
        }

        public Task Add(SimplePost model)
        {
            var entity = mapper.Map<Post>(model);

            entity.UserId = UserId;

            context.Posts.Add(entity);

            return context.SaveChangesAsync();
        }

        public Task Delete(int id) => context.Posts
            .Where(p =>
                p.Id == id &&
                p.UserId == UserId
             )
            .ExecuteDeleteAsync();
    }
}
