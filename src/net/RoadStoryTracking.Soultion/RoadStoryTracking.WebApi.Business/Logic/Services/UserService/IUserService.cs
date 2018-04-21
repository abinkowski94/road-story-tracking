using System;
using System.Threading.Tasks;
using RoadStoryTracking.WebApi.Business.Models.Responses;
using RoadStoryTracking.WebApi.Business.Models.User;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.UserService
{
    public interface IUserService : IDisposable
    {
        Task<BaseResponse> ConfirmUserEmailAddress(string userName, string confirmationToken);

        Task<BaseResponse> CreateToken(string username, string password);

        Task<BaseResponse> GetUser(string userName);

        Task<BaseResponse> RegisterNewUser(ApplicationUser ApplicationUser, Uri tokenCallback);

        Task<BaseResponse> UpdateUser(string userName, ApplicationUser ApplicationUser);

        Task<BaseResponse> UpdateUserPassword(string userName, string oldPassword, string newPassword);
    }
}