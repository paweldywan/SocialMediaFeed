using SocialMediaFeed.DAL.Models;

namespace SocialMediaFeed.BLL.Models
{
    public class PostDto : SimplePost
    {
        public string? UserName { get; set; }

        public bool CanDelete { get; set; }

        public bool CanEdit { get; set; }

        public int? LikesCount { get; set; }

        public bool? Liked { get; set; }

        public IEnumerable<PostDto>? Posts { get; set; }
    }
}
