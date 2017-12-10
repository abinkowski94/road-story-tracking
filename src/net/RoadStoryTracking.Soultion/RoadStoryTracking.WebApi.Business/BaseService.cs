using AutoMapper;
using RoadStoryTracking.WebApi.Business.BusinessModels.User;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BME = RoadStoryTracking.WebApi.Business.BusinessModels.Exceptions;
using RoadStoryTracking.WebApi.Business.BusinessModels.Marker;
using System.Linq;
using System;

namespace RoadStoryTracking.WebApi.Business
{
    public class BaseService
    {
        public IMapper LocalMapper { get; private set; }

        public BaseService()
        {
            LocalMapper = ConfigureMapper().CreateMapper();
        }

        protected BaseResponse ValidateModel<T>(T model)
        {
            var ctx = new System.ComponentModel.DataAnnotations.ValidationContext(model);
            var errors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(model, ctx, errors, true))
            {
                return new ErrorResponse(new BME.ValidationException("Errors occurred during validation of model", errors));
            }

            return new SuccessResponse<bool>(true);
        }

        private MapperConfiguration ConfigureMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, Data.Models.ApplicationUser>()
                    .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                    .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                    .ForAllOtherMembers(opt => opt.Ignore());

                cfg.CreateMap<Data.Models.ApplicationUser, ApplicationUser>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                    .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                    .ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                    .ForAllOtherMembers(opt => opt.Ignore());

                cfg.CreateMap<Data.Models.ApplicationUser, MarkerOwner>()
                    .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                    .ForMember(dst => dst.FirsName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForAllOtherMembers(dst => dst.Ignore());

                cfg.CreateMap<Data.Models.Marker, Marker>()
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dst => dst.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                    .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dst => dst.Latitude, opt => opt.MapFrom(src => src.Latitude))
                    .ForMember(dst => dst.Longitude, opt => opt.MapFrom(src => src.Longitude))
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Type))
                    .ForMember(dst => dst.MarkerOwner, opt => opt.MapFrom(src => src.ApplicationUser))
                    .ForMember(dst => dst.Images, opt => opt.ResolveUsing(src => src.Images?.Select(img => img.Image)?.ToList()))
                    .ForAllOtherMembers(dst => dst.Ignore());

                cfg.CreateMap<Marker, Data.Models.Marker>()
                    .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dst => dst.Latitude, opt => opt.MapFrom(src => src.Latitude))
                    .ForMember(dst => dst.Longitude, opt => opt.MapFrom(src => src.Longitude))
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Type))
                    .ForMember(dst => dst.CreateDate, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
                    .ForMember(dst => dst.Images, opt => opt.ResolveUsing(src => src.Images
                        .Select(img => new Data.Models.MarkerImage { Image = img, CreateDate = DateTimeOffset.UtcNow }).ToList()))
                    .ForAllOtherMembers(dst => dst.Ignore());
            });

            configuration.AssertConfigurationIsValid();

            return configuration;
        }
    }
}