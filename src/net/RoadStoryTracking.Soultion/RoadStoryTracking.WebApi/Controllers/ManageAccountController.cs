using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Models.User;
using RoadStoryTracking.WebApi.Business.Logic.Services.UserService;
using RoadStoryTracking.WebApi.Extensions;
using System;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ManageAccountController : BaseController
    {
        private readonly IUserService _userService;

        public ManageAccountController(IServiceProvider serviceProvider, IUserService userService) : base(serviceProvider)
        {
            _userService = userService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserData()
        {
            var response = await _userService.GetUser(Requestor.UserName);
            return response.GetActionResult<Business.Models.User.ApplicationUser, ApplicationUser>(this);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUserData([FromBody] ApplicationUser applicationUser)
        {
            var mappedUser = LocalMapper.Map<Business.Models.User.ApplicationUser>(applicationUser);
            var response = await _userService.UpdateUser(Requestor.UserName, mappedUser);
            return response.GetActionResult<Business.Models.User.ApplicationUser, ApplicationUser>(this);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUserPassword(string oldPassword, string newPassword)
        {
            var response = await _userService.UpdateUserPassword(Requestor.User.UserName, oldPassword, newPassword);
            return response.GetActionResult(this);
        }
    }
}