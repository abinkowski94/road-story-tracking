using System;
using System.Threading.Tasks;
using RoadStoryTracking.WebApi.Business.Logic.Services.UserService;
using RoadStoryTracking.WebApi.Business.Models.Responses;
using RoadStoryTracking.WebApi.Business.Models.User;

namespace RoadStoryTracking.WebApi.Models
{
    public class Requestor
    {
        private readonly IServiceProvider _serviceProvider;

        public ApplicationUser User { get; private set; }
        public string UserName { get; private set; }

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
                var userService = (_serviceProvider.GetService(typeof(IUserService)) as IUserService);
                var response = await userService.GetUser(UserName) as SuccessResponse<ApplicationUser>;
                User = response.Result;
            });
        }
    }
}