using System.ComponentModel.DataAnnotations;

namespace SocialMediaFeed.BLL.Models
{
    public class SimplePost
    {
        [Required]
        public required string Content { get; set; }
    }
}
