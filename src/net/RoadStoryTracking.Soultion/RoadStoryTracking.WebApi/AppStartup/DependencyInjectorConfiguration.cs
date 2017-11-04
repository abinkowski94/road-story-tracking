using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadStoryTracking.WebApi.Business.EmailService;
using RoadStoryTracking.WebApi.Business.UserService;

namespace RoadStoryTracking.WebApi.AppStartup
{
    public static class DependencyInjectorConfiguration
    {
        public static void ConfigureDependencyInjector(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddSingleton(configuration);
        }
    }
}