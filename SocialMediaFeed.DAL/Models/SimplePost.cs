using SocialMediaFeed.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialMediaFeed.DAL.Models
{
    public class SimplePost
    {
        public int Id { get; set; }

        [Required]
        [JsonIgnore]
        public string? UserId { get; set; }

        [JsonIgnore]
        public int? PostId { get; set; }

        public required string Content { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        [JsonIgnore]
        public virtual ICollection<Like>? Likes { get; set; }
    }
}
