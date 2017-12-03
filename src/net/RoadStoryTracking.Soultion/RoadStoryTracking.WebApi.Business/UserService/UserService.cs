using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RoadStoryTracking.WebApi.Business.BusinessModels;
using RoadStoryTracking.WebApi.Business.BusinessModels.Exceptions;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using RoadStoryTracking.WebApi.Business.EmailService;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Entities = RoadStoryTracking.WebApi.Data.Models;

namespace RoadStoryTracking.WebApi.Business.UserService
{
    public class UserService : BaseService, IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IPasswordHasher<Entities.ApplicationUser> _passwordHasher;
        private readonly UserManager<Entities.ApplicationUser> _userManager;

        public UserService(IConfiguration configuration, IPasswordHasher<Entities.ApplicationUser> passwordHasher,
            UserManager<Entities.ApplicationUser> userService, IEmailService emailService) : base()
        {
            _userManager = userService;
            _emailService = emailService;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public async Task<BaseResponse> ConfirmUserEmailAddress(string userName, string confirmationToken)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var response = await _userManager.ConfirmEmailAsync(user, confirmationToken);

                if (response.Succeeded)
                {
                    return new SuccessResponse<object>(true);
                }
                else
                {
                    var errorMessage = "Multiple errors occured during confimation of user email address.";
                    var errors = response.Errors.Select(e => new CustomApplicationException(e.Description)).ToArray();
                    return new ErrorResponse(new CustomAggregatedException(errorMessage, errors));
                }
            }

            return new ErrorResponse(new CustomApplicationException("Wrong user name or confirmation token!"));
        }

        public async Task<BaseResponse> CreateToken(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return new ErrorResponse(new CustomApplicationException("Username cannot be null"));
            }
            if (string.IsNullOrEmpty(password))
            {
                return new ErrorResponse(new CustomApplicationException("Password cannot be null"));
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                if (!await _userManager.IsLockedOutAsync(user))
                {
                    if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success)
                    {
                        if (await _userManager.IsEmailConfirmedAsync(user))
                        {
                            await _userManager.ResetAccessFailedCountAsync(user);
                            return new SuccessResponse<TokenInfo>(await GetTokenInfo(user));
                        }
                        else
                        {
                            return new ErrorResponse(new CustomApplicationException("Plase first confirm your email before login."));
                        }
                    }
                    else
                    {
                        await _userManager.AccessFailedAsync(user);
                    }
                }
                else
                {
                    var lockoutTime = await _userManager.GetLockoutEndDateAsync(user);
                    var message = $"Your account has been blocked due to too many failed login attempts. Lockout end date: {lockoutTime.Value}";
                    return new ErrorResponse(new CustomApplicationException(message));
                }
            }

            return new ErrorResponse(new CustomApplicationException("Wrong user name or password!"));
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }

        public async Task<BaseResponse> GetUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var mappedUser = LocalMapper.Map<ApplicationUser>(user);
                return new SuccessResponse<ApplicationUser>(mappedUser);
            }

            return new ErrorResponse(new CustomApplicationException("User with given name not found."));
        }

        public async Task<BaseResponse> RegisterNewUser(ApplicationUser applicationUser, Uri tokenCallback)
        {
            var mappedApplicationUser = LocalMapper.Map<Entities.ApplicationUser>(applicationUser);
            var createdUser = await _userManager.CreateAsync(mappedApplicationUser, applicationUser.Password);

            if (createdUser != null && createdUser.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(mappedApplicationUser);
                await SendEmailConfirmation(applicationUser.Email, $"{applicationUser.FirstName} {applicationUser.LastName}", tokenCallback, applicationUser.UserName, token);
                return new SuccessResponse<ApplicationUser>(applicationUser);
            }
            else
            {
                var exceptions = createdUser.Errors.Select(e => new CustomApplicationException(e.Description));
                return new ErrorResponse(new CustomAggregatedException("Exceptions occured during creating new user.", exceptions.ToArray()));
            }
        }

        public async Task<BaseResponse> UpdateUser(string userName, ApplicationUser ApplicationUser)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                user.FirstName = ApplicationUser.FirstName;
                user.LastName = ApplicationUser.LastName;
                user.PhoneNumber = ApplicationUser.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var mappedUser = LocalMapper.Map<ApplicationUser>(user);
                    return new SuccessResponse<ApplicationUser>(mappedUser);
                }
                var errors = result.Errors.Select(e => new CustomApplicationException(e.Description)).ToArray();
                return new ErrorResponse(new CustomAggregatedException("Errors occured during user update.", errors));
            }
            return new ErrorResponse(new CustomApplicationException($"Could not find user with user name {userName}"));
        }

        public async Task<BaseResponse> UpdateUserPassword(string userName, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword) == PasswordVerificationResult.Success)
                {
                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                    if (result.Succeeded)
                    {
                        return new SuccessResponse<object>(true);
                    }
                    var errors = result.Errors.Select(e => new CustomApplicationException(e.Description)).ToArray();
                    return new ErrorResponse(new CustomAggregatedException("Errors occured during changing password.", errors));
                }
                return new ErrorResponse(new CustomApplicationException("Wrong old password."));
            }
            return new ErrorResponse(new CustomApplicationException($"Could not find user with user name: '{userName}'."));
        }

        private async Task<TokenInfo> GetTokenInfo(Entities.ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString())
            }
            .Union(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Tokens:Issuer"],
                audience: _configuration["Tokens:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenInfo(generatedToken, token.ValidTo, user.UserName);
        }

        private async Task SendEmailConfirmation(string email, string fullName, Uri tokenCallback, string userName, string confirmationToken)
        {
            var uriBuilder = new UriBuilder(tokenCallback);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["userName"] = userName;
            query["token"] = confirmationToken;
            uriBuilder.Query = query.ToString();
            var tokenCallbackUrl = uriBuilder.ToString();

            var messageText = $"Please confirm your registration by clicking this link: {tokenCallbackUrl}";
            var messageHTML = $"<h2>Please confirm your registration by clicking this link:</h2><br>{tokenCallbackUrl}";
            await _emailService.SendEmail(email, fullName, "Email confirmation", messageText, messageHTML);
        }
    }
}