using SocialMediaFeed.BLL.Models;
using SocialMediaFeed.DAL.Entities;

namespace SocialMediaFeed.BLL.Interfaces
{
    public interface IPostService
    {
        Task Add(SimplePost post);
        Task<List<Post>> Get();
    }
}