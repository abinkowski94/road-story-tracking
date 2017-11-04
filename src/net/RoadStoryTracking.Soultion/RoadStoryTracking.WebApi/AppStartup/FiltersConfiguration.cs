using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RoadStoryTracking.WebApi.Filters;

namespace RoadStoryTracking.WebApi.AppStartup
{
    public static class FiltersConfiguration
    {
        public static void ConfigureFilter(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ErrorFilterAttribute());
                options.Filters.Add(new RequireHttpsAttribute());
            });
        }
    }
}