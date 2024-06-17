namespace SocialMediaFeed.DAL.Entities
{
    public class Like
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public required string UserId { get; set; }
    }
}
