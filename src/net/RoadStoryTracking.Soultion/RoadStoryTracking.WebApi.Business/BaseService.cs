using AutoMapper;
using RoadStoryTracking.WebApi.Business.BusinessModels;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BME = RoadStoryTracking.WebApi.Business.BusinessModels.Exceptions;

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
                    .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.UserName))
                    .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                    .ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                    .ForAllOtherMembers(opt => opt.Ignore());
            });

            configuration.AssertConfigurationIsValid();

            return configuration;
        }
    }
}