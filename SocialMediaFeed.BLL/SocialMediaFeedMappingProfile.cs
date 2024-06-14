using AutoMapper;
using SocialMediaFeed.BLL.Models;
using SocialMediaFeed.DAL.Entities;

namespace SocialMediaFeed.BLL
{
    public class SocialMediaFeedMappingProfile : Profile
    {
        public SocialMediaFeedMappingProfile()
        {
            CreateMap<SimplePost, Post>()
                .ForMember(p => p.CreatedAt, opt => opt.MapFrom(sp => DateTimeOffset.UtcNow));
        }
    }
}
