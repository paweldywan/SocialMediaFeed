using SocialMediaFeed.DAL.Entities;

namespace SocialMediaFeed.BLL.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> Get();
    }
}