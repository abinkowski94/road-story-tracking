using AutoMapper;
using RoadStoryTracking.Model.Models.Comment;

namespace RoadStoryTracking.WebApi.Controllers.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Business.Models.Comment.Comment, Comment>().ReverseMap();
            CreateMap<Business.Models.Comment.CommentAuthor, CommentAuthor>().ReverseMap();
        }
    }
}