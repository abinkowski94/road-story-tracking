using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RoadStoryTracking.Model.Models.Comment;
using RoadStoryTracking.Model.Models.Marker;
using RoadStoryTracking.Model.Models.User;
using RoadStoryTracking.Model.Responses;
using RoadStoryTracking.WebApi.Controllers.MappingProfiles;
using RoadStoryTracking.WebApi.Data.Context;
using RoadStoryTracking.WebApi.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BMC = RoadStoryTracking.WebApi.Business.BusinessModels.Comment;
using BMM = RoadStoryTracking.WebApi.Business.BusinessModels.Marker;
using BMR = RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using BMU = RoadStoryTracking.WebApi.Business.BusinessModels.User;

namespace RoadStoryTracking.WebApi.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IServiceProvider _serviceProvider;

        public IMapper LocalMapper { get; private set; }
        protected Requestor Requestor { get; private set; }

        protected BaseController(IServiceProvider serviceProvider)
        {
            LocalMapper = ConfigureMapper().CreateMapper();
            _serviceProvider = serviceProvider;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dbContext = (RoadStoryTrackingDbContext)_serviceProvider.GetService(typeof(RoadStoryTrackingDbContext));
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
                cfg.AddProfile<CommentProfile>();
                cfg.AddProfile<MarkerProfile>();
                cfg.AddProfile<TechnicalProfile>();
                cfg.AddProfile<UserProfile>();
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