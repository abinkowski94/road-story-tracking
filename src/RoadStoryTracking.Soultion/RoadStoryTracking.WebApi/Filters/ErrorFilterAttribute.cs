using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RoadStoryTracking.Model.Responses;
using System.Net;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Filters
{
    public class ErrorFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var client = new TelemetryClient();
            client.TrackException(context.Exception);
            context.Result = new JsonResult(new ErrorResponse(context.Exception))
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            return Task.Run(() => OnException(context));
        }
    }
}