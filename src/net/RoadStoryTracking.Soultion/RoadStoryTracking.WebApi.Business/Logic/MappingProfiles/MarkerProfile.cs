using AutoMapper;
using RoadStoryTracking.WebApi.Business.Models.Marker;
using System;
using System.Linq;

namespace RoadStoryTracking.WebApi.Business.Logic.MappingProfiles
{
    internal class MarkerProfile : Profile
    {
        public MarkerProfile()
        {
            CreateMap<Data.Models.ApplicationUser, MarkerOwner>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.Image, opt => opt.MapFrom(src => src.ImageUrl))
                .ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<Data.Models.Marker, Marker>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dst => dst.Longitude, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dst => dst.MarkerOwner, opt => opt.MapFrom(src => src.ApplicationUser))
                .ForMember(dst => dst.IsPrivate, opt => opt.MapFrom(src => src.IsPrivate))
                .ForMember(dst => dst.MarkerInvitations, opt => opt.MapFrom(src => src.MarkerInvitations))
                .ForMember(dst => dst.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dst => dst.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dst => dst.Images, opt => opt.ResolveUsing(src => src.Images?.Select(img => img.Image).ToList()))
                .ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<Marker, Data.Models.Marker>()
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dst => dst.Longitude, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dst => dst.IsPrivate, opt => opt.MapFrom(src => src.IsPrivate))
                .ForMember(dst => dst.CreateDate, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
                .ForMember(dst => dst.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dst => dst.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dst => dst.Images, opt => opt.ResolveUsing(src => src.Images
                    .Select(img => new Data.Models.MarkerImage { Image = img, CreateDate = DateTimeOffset.UtcNow }).ToList()))
                .ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<Data.Models.MarkerInvitation, MarkerInvitation>()
                .ForMember(dst => dst.InvitedUserFirstName, opt => opt.MapFrom(src => src.InvitedUser.FirstName))
                .ForMember(dst => dst.InvitedUserImage, opt => opt.MapFrom(src => src.InvitedUser.ImageUrl))
                .ForMember(dst => dst.InvitedUserLastName, opt => opt.MapFrom(src => src.InvitedUser.LastName))
                .ForMember(dst => dst.InvitedUserUserName, opt => opt.MapFrom(src => src.InvitedUser.UserName))
                .ForMember(dst => dst.InvitationStatus, opt => opt.MapFrom(src => src.InvitationStatus))
                .ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<Data.Models.MarkerInvitation, IncomingMarkerInviation>()
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Marker.Description))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.InvitationStatus, opt => opt.MapFrom(src => src.InvitationStatus))
                .ForMember(dst => dst.MarkerId, opt => opt.MapFrom(src => src.MarkerId))
                .ForMember(dst => dst.MarkerOwner, opt => opt.MapFrom(src => src.Marker.ApplicationUser))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Marker.Name))
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}