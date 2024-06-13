using Microsoft.AspNetCore.Identity;

namespace SocialMediaFeed.DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public required string UserId { get; set; }

        public virtual IdentityUser? User { get; set; }
    }
}
