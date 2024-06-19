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

            result.ForEach(SetPostPermissions);

            var postsLookup = result.ToLookup(p => p.PostId);

            CreatePostsTree(result, postsLookup);

            return result.FindAll(p => p.PostId == null);
        }

        private static void CreatePostsTree(IEnumerable<PostDto> posts, ILookup<int?, PostDto> postsLookup)
        {
            foreach (var post in posts)
            {
                post.Posts = postsLookup[post.Id];

                CreatePostsTree(post.Posts, postsLookup);
            }
        }

        private void SetPostPermissions(PostDto post)
        {
            post.CanDelete = post.CanEdit = post.UserId == UserId;

            post.Liked = post.Likes?.Any(l => l.UserId == UserId);
        }

        public Task Add(PostToAdd model)
        {
            var entity = mapper.Map<Post>(model);

            entity.UserId = UserId;

            context.Posts.Add(entity);

            return context.SaveChangesAsync();
        }

        public async Task Update(PostToUpdate model)
        {
            var post = await context.Posts
                .SingleOrDefaultAsync(p =>
                    p.Id == model.Id &&
                    p.UserId == UserId
                 );

            if (post == null)
                return;

            mapper.Map(model, post);

            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = await context.Posts
                .Include(p => p.Posts)
                .SingleOrDefaultAsync(p => p.Id == id && p.UserId == UserId);

            if (post == null)
                return;

            await DeletePostAndSubPosts(post);

            await context.SaveChangesAsync();
        }

        private async Task DeletePostAndSubPosts(Post post)
        {
            await context.Entry(post)
                 .Collection(p => p.Posts)
                 .LoadAsync();

            foreach (var subPost in post.Posts)
            {
                await DeletePostAndSubPosts(subPost);
            }

            context.Posts.Remove(post);
        }

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
