using AutoMapper;
using RoadStoryTracking.WebApi.Business.Models.Comment;
using System;

namespace RoadStoryTracking.WebApi.Business.Logic.MappingProfiles
{
    internal class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Data.Models.ApplicationUser, CommentAuthor>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<Data.Models.Comment, Comment>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.MarkerId, opt => opt.MapFrom(src => src.MarkerId))
                .ForMember(dst => dst.ModificationDate, opt => opt.MapFrom(src => src.ModificationDate))
                .ForMember(dst => dst.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dst => dst.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                .ForMember(dst => dst.CommentAuthor, opt => opt.MapFrom(src => src.ApplicationUser))
                .ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<Comment, Data.Models.Comment>()
                .ForMember(dst => dst.MarkerId, opt => opt.MapFrom(src => src.MarkerId))
                .ForMember(dst => dst.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dst => dst.CreateDate, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}