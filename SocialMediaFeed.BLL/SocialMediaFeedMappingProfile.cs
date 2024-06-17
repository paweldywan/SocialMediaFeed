using AutoMapper;
using SocialMediaFeed.BLL.Models;
using SocialMediaFeed.DAL.Entities;

namespace SocialMediaFeed.BLL
{
    public class SocialMediaFeedMappingProfile : Profile
    {
        public SocialMediaFeedMappingProfile()
        {
            CreateMap<PostToAdd, Post>()
                .ForMember(p => p.CreatedAt, opt => opt.MapFrom(sp => DateTimeOffset.UtcNow));

            CreateMap<Post, PostDto>()
                .ForMember(p => p.UserName, opt => opt.MapFrom(p => p.User == null ? null : p.User.UserName))
                .ForMember(p => p.LikesCount, opt => opt.MapFrom(p => p.Likes == null ? (int?)null : p.Likes.Count));

            CreateMap<PostToUpdate, Post>();
        }
    }
}
