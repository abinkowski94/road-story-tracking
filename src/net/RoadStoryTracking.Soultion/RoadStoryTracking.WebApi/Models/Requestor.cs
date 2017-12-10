using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using RoadStoryTracking.WebApi.Business.BusinessModels.User;
using RoadStoryTracking.WebApi.Business.UserService;
using System;
using System.Threading.Tasks;

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

        private async Task FillRequestorAsync()
        {
            var userService = (_serviceProvider.GetService(typeof(IUserService)) as IUserService);
            var response = await userService.GetUser(UserName) as SuccessResponse<ApplicationUser>;
            User = response.Result;
        }
    }
}