using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RoadStoryTracking.Model.Models.User;
using RoadStoryTracking.Model.Responses;
using RoadStoryTracking.WebApi.Data.Context;
using RoadStoryTracking.WebApi.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BMU = RoadStoryTracking.WebApi.Business.BusinessModels.User;
using BMM = RoadStoryTracking.WebApi.Business.BusinessModels.Marker;
using BMR = RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using RoadStoryTracking.Model.Models.Marker;

namespace RoadStoryTracking.WebApi.Controllers
{
    public abstract class BaseController : Controller
    {
        public IMapper LocalMapper { get; private set; }
        protected Requestor Requestor { get; private set; }
        protected IServiceProvider ServiceProvider { get; private set; }

        protected BaseController(IServiceProvider serviceProvider)
        {
            LocalMapper = ConfigureMapper().CreateMapper();
            ServiceProvider = serviceProvider;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dbContext = (RoadStoryTrackingDbContext)ServiceProvider.GetService(typeof(RoadStoryTrackingDbContext));
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    SetupRequestor();
                    var response = await next();
                    if (response.Result is ObjectResult result && result.StatusCode.Equals(HttpStatusCode.BadRequest))
                    {
                        transaction.Rollback();
                    }
                    else
                    {
                        transaction.Commit();
                    }
                }
                catch (Exception exception)
                {
                    Trace.TraceError(exception.Message);
                    Trace.TraceError(exception.StackTrace);
                    transaction.Rollback();
                }
            }
        }

        private MapperConfiguration ConfigureMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(typeof(BMR.BaseResponse), typeof(BaseResponse));
                cfg.CreateMap(typeof(BMR.SuccessResponse<>), typeof(SuccessResponse<>));
                cfg.CreateMap(typeof(BMR.ErrorResponse), typeof(ErrorResponse));

                cfg.CreateMap<BMU.TokenInfo, TokenInfo>();

                cfg.CreateMap<BMU.ApplicationUser, ApplicationUser>();
                cfg.CreateMap<ApplicationUser, BMU.ApplicationUser>()
                    .ForMember(p => p.UserName, p => p.MapFrom(d => d.Email))
                    .ForMember(p => p.Id, p => p.Ignore());

                cfg.CreateMap<BMM.Marker, Marker>().ReverseMap();
                cfg.CreateMap<BMM.MarkerOwner, MarkerOwner>().ReverseMap();
            });

            configuration.AssertConfigurationIsValid();

            return configuration;
        }

        private void SetupRequestor()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var userName = User.Claims.Where(c => c.Properties.Values.Contains("sub")).ToList().First().Value;
                var serviceProvider = HttpContext.RequestServices;
                Requestor = new Requestor(userName, serviceProvider);
            }
        }
    }
}