using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaFeed.DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public required string UserId { get; set; }

        public virtual IdentityUser? User { get; set; }

        [NotMapped]
        public bool CanDelete { get; set; }
    }
}
