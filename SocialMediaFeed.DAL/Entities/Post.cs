using Microsoft.AspNetCore.Identity;
using SocialMediaFeed.DAL.Models;

namespace SocialMediaFeed.DAL.Entities
{
    public class Post : SimplePost
    {
        public virtual IdentityUser? User { get; set; }
    }
}
