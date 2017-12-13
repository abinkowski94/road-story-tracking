using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadStoryTracking.WebApi.Business.EmailService;
using RoadStoryTracking.WebApi.Business.ImageService;
using RoadStoryTracking.WebApi.Business.MarkerService;
using RoadStoryTracking.WebApi.Business.UserService;
using RoadStoryTracking.WebApi.Data.Repositories;

namespace RoadStoryTracking.WebApi.AppStartup
{
    public static class DependencyInjectorConfiguration
    {
        public static void ConfigureDependencyInjector(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMarkerRepository, MarkerRepository>();
            services.AddTransient<IMarkerService, MarkerService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddSingleton(configuration);
        }
    }
}