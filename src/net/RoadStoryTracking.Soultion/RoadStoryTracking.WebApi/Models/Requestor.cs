using RoadStoryTracking.WebApi.Business.Logic.Services.UserService;
using RoadStoryTracking.WebApi.Business.Models.Responses;
using RoadStoryTracking.WebApi.Business.Models.User;
using System;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Models
{
    public class Requestor
    {
        private readonly IServiceProvider _serviceProvider;

        public ApplicationUser User { get; private set; }
        public string UserName { get; }

        public Requestor(string userName, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            UserName = userName;
            FillRequestorAsync().Wait();
        }

        private Task FillRequestorAsync()
        {
            return Task.Run(async () =>
            {
                if (_serviceProvider.GetService(typeof(IUserService)) is IUserService userService)
                {
                    if (await userService.GetUser(UserName) is SuccessResponse<ApplicationUser> response)
                    {
                        User = response.Result;
                    }
                }
            });
        }
    }
}