using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadStoryTracking.WebApi.Data.Context;

namespace RoadStoryTracking.WebApi.AppStartup
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabae(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RoadStoryTrackingDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}