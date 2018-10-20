using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadStoryTracking.WebApi.Business.Logic.Services.CommentService;
using RoadStoryTracking.WebApi.Business.Logic.Services.ContctService;
using RoadStoryTracking.WebApi.Business.Logic.Services.EmailService;
using RoadStoryTracking.WebApi.Business.Logic.Services.ImageService;
using RoadStoryTracking.WebApi.Business.Logic.Services.MarkerService;
using RoadStoryTracking.WebApi.Business.Logic.Services.UserService;
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
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IContactsRepository, ContactsRepository>();
            services.AddTransient<IContactService, ContactService>();
            services.AddSingleton(configuration);
        }
    }
}