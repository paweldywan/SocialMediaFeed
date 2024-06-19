using System.ComponentModel.DataAnnotations;

namespace SocialMediaFeed.BLL.Models
{
    public class PostToAdd
    {
        [Required]
        public required string Content { get; set; }

        public int? PostId { get; set; }
    }
}
