using SocialMediaFeed.BLL.Models;

namespace SocialMediaFeed.BLL.Interfaces
{
    public interface IPostService
    {
        Task Add(PostToAdd post);
        Task Delete(int id);
        Task<List<PostDto>> Get();
        Task ToggleLike(int id);
    }
}