using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task<List<PostDto>> Get()
        {
            var result = await context.Posts
                .Include(p => p.User)
                .Include(p => p.Likes)
                .OrderByDescending(p => p.CreatedAt)
                .ProjectTo<PostDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            result.ForEach(p => p.CanDelete = p.UserId == UserId);

            result.ForEach(p => p.Liked = p.Likes?.Any(l => l.UserId == UserId));

            return result;
        }

        public Task Add(PostToAdd model)
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

        public async Task ToggleLike(int id)
        {
            var post = await context.Posts
                .Include(p => p.Likes)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return;

            var like = post.Likes?.SingleOrDefault(l => l.UserId == UserId);

            if (like == null)
            {
                post.Likes ??= [];

                post.Likes.Add(new Like
                {
                    UserId = UserId
                });
            }
            else
            {
                context.Likes.Remove(like);
            }

            await context.SaveChangesAsync();
        }
    }
}
