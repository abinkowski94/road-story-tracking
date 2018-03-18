using AutoMapper;
using RoadStoryTracking.Model.Models.Comment;
using BMC = RoadStoryTracking.WebApi.Business.BusinessModels.Comment;

namespace RoadStoryTracking.WebApi.Controllers.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<BMC.Comment, Comment>().ReverseMap();
            CreateMap<BMC.CommentAuthor, CommentAuthor>().ReverseMap();
        }
    }
}