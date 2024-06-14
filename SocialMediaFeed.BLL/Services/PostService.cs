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

        public Task<List<Post>> Get() => context.Posts
            .Include(p => p.User)
            .ToListAsync();

        public Task Add(SimplePost model)
        {
            var entity = mapper.Map<Post>(model);

            entity.UserId = UserId;

            context.Posts.Add(entity);

            return context.SaveChangesAsync();
        }
    }
}
