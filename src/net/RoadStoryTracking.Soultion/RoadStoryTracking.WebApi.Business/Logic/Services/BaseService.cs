using AutoMapper;
using RoadStoryTracking.WebApi.Business.Logic.MappingProfiles;
using RoadStoryTracking.WebApi.Business.Models.Responses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValidationException = RoadStoryTracking.WebApi.Business.Models.Exceptions.ValidationException;

namespace RoadStoryTracking.WebApi.Business.Logic.Services
{
    public abstract class BaseService
    {
        public static IMapper LocalMapper { get; private set; }

        static BaseService()
        {
            LocalMapper = ConfigureMapper().CreateMapper();
        }

        protected BaseResponse ValidateModel<T>(T model)
        {
            var ctx = new System.ComponentModel.DataAnnotations.ValidationContext(model);
            var errors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(model, ctx, errors, true))
            {
                return new ErrorResponse(new ValidationException("Errors occurred during validation of model", errors));
            }

            return new SuccessResponse<bool>(true);
        }

        private static MapperConfiguration ConfigureMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<MarkerProfile>();
                cfg.AddProfile<CommentProfile>();
            });

            configuration.AssertConfigurationIsValid();

            return configuration;
        }
    }
}