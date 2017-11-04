using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Models;
using RoadStoryTracking.WebApi.Business.UserService;
using RoadStoryTracking.WebApi.Extensions;
using System;
using System.Threading.Tasks;
using BM = RoadStoryTracking.WebApi.Business.BusinessModels;

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
            return response.GetActionResult<BM.ApplicationUser, ApplicationUser>(this);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUserData([FromBody] ApplicationUser ApplicationUser)
        {
            var mappedUser = LocalMapper.Map<BM.ApplicationUser>(ApplicationUser);
            var response = await _userService.UpdateUser(Requestor.UserName, mappedUser);
            return response.GetActionResult<BM.ApplicationUser, ApplicationUser>(this);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUserPassword(string oldPassword, string newPassword)
        {
            var response = await _userService.UpdateUserPassword(Requestor.UserName, oldPassword, newPassword);
            return response.GetActionResult(this);
        }
    }
}