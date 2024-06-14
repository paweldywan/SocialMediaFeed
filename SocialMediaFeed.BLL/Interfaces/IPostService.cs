using SocialMediaFeed.BLL.Models;
using SocialMediaFeed.DAL.Entities;

namespace SocialMediaFeed.BLL.Interfaces
{
    public interface IPostService
    {
        Task Add(SimplePost post);
        Task Delete(int id);
        Task<List<Post>> Get();
    }
}