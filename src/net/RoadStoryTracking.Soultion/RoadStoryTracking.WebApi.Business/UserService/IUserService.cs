using RoadStoryTracking.WebApi.Business.BusinessModels.User;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using System;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.UserService
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