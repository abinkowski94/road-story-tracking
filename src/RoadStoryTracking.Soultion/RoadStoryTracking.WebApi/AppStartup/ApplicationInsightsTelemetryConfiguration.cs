using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RoadStoryTracking.WebApi.AppStartup
{
    public static class ApplicationInsightsTelemetryConfiguration
    {
        public static void ConfigureApplicationInsightsTelemetry(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry(configuration);
        }
    }
}