using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RoadStoryTracking.WebApi.Business.Logic.Services.EmailService;
using RoadStoryTracking.WebApi.Business.Models.Exceptions;
using RoadStoryTracking.WebApi.Business.Models.Messages;
using RoadStoryTracking.WebApi.Business.Models.Responses;
using RoadStoryTracking.WebApi.Business.Models.User;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using static System.String;
using ApplicationUser = RoadStoryTracking.WebApi.Business.Models.User.ApplicationUser;
using Entities = RoadStoryTracking.WebApi.Data.Models;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.UserService
{
    public class UserService : BaseService, IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IPasswordHasher<Entities.ApplicationUser> _passwordHasher;
        private readonly UserManager<Entities.ApplicationUser> _userManager;

        public UserService(IConfiguration configuration, IPasswordHasher<Entities.ApplicationUser> passwordHasher,
            UserManager<Entities.ApplicationUser> userService, IEmailService emailService)
        {
            _userManager = userService;
            _emailService = emailService;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public async Task<BaseResponse> ConfirmUserEmailAddress(string userName, string confirmationToken)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return new ErrorResponse(new CustomApplicationException("Wrong user name or confirmation token!"));
            }

            var response = await _userManager.ConfirmEmailAsync(user, confirmationToken);
            if (response.Succeeded)
            {
                return new SuccessResponse<object>(true);
            }

            const string errorMessage = "Multiple errors occured during confimation of user email address.";
            var errors = response.Errors.Select(e => new CustomApplicationException(e.Description)).ToArray();
            return new ErrorResponse(new CustomAggregatedException(errorMessage, errors));
        }

        public async Task<BaseResponse> CreateToken(string username, string password)
        {
            if (IsNullOrEmpty(username))
            {
                return new ErrorResponse(new CustomApplicationException("Username cannot be null"));
            }
            if (IsNullOrEmpty(password))
            {
                return new ErrorResponse(new CustomApplicationException("Password cannot be null"));
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return new ErrorResponse(new CustomApplicationException("Wrong user name or password!"));
            }

            if (!await _userManager.IsLockedOutAsync(user))
            {
                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success)
                {
                    if (await _userManager.IsEmailConfirmedAsync(user))
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        return new SuccessResponse<TokenInfo>(await GetTokenInfo(user));
                    }

                    return new ErrorResponse(new CustomApplicationException("Plase first confirm your email before login."));
                }

                await _userManager.AccessFailedAsync(user);
            }
            else
            {
                var lockoutTime = await _userManager.GetLockoutEndDateAsync(user);
                var message = $"Your account has been blocked due to too many failed login attempts. Lockout end date: {lockoutTime.Value}";
                return new ErrorResponse(new CustomApplicationException(message));
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
            if (user == null)
            {
                return new ErrorResponse(new CustomApplicationException("User with given name not found."));
            }

            var mappedUser = LocalMapper.Map<ApplicationUser>(user);
            return new SuccessResponse<ApplicationUser>(mappedUser);
        }

        public async Task<BaseResponse> RegisterNewUser(ApplicationUser applicationUser, Uri tokenCallback)
        {
            var mappedApplicationUser = LocalMapper.Map<Entities.ApplicationUser>(applicationUser);
            var createdUser = await _userManager.CreateAsync(mappedApplicationUser, applicationUser.Password);

            if (createdUser == null)
            {
                return new ErrorResponse(new ApplicationException("Created user is null!"));
            }
            if (createdUser.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(mappedApplicationUser);
                await SendEmailConfirmation(applicationUser.Email, $"{applicationUser.FirstName} {applicationUser.LastName}", tokenCallback, applicationUser.UserName, token);
                return new SuccessResponse<ApplicationUser>(applicationUser);
            }

            var exceptions = createdUser.Errors.Select(e => new CustomApplicationException(e.Description));
            return new ErrorResponse(new CustomAggregatedException("Exceptions occurred during creating new user.", exceptions.ToArray()));
        }

        public async Task<BaseResponse> ResetPassword(string userName, Uri callbackUri)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new SuccessResponse<string>("OK");
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            await SendEmailReset(user.Email, $"{user.FirstName} {user.LastName}", callbackUri, user.UserName, resetToken);

            return new SuccessResponse<string>("OK");
        }

        public async Task<BaseResponse> UpdateUser(string userName, ApplicationUser applicationUser)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new ErrorResponse(new CustomApplicationException($"Could not find user with user name {userName}"));
            }

            user.FirstName = applicationUser.FirstName;
            user.LastName = applicationUser.LastName;
            user.PhoneNumber = applicationUser.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var mappedUser = LocalMapper.Map<ApplicationUser>(user);
                return new SuccessResponse<ApplicationUser>(mappedUser);
            }

            var errors = result.Errors.Select(e => new CustomApplicationException(e.Description)).ToArray();
            return new ErrorResponse(new CustomAggregatedException("Errors occurred during user update.", errors));
        }

        public async Task<BaseResponse> UpdateUserPassword(string userName, string token, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new ErrorResponse(new CustomApplicationException($"Could not find user with user name: '{userName}'."));
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (result.Succeeded)
            {
                return new SuccessResponse<object>(true);
            }

            var errors = result.Errors.Select(e => new CustomApplicationException(e.Description)).ToArray();
            return new ErrorResponse(new CustomAggregatedException("Errors occured during changing password.", errors));
        }

        private string CreateHtmlMessage(string userName, string callbackUrl, string mainMessage)
        {
            var emailMessage = new EmailMessage
            {
                UserName = userName,
                CallbackUrl = callbackUrl,
                MainMessage = mainMessage
            };

            var xml = CreateXmlFromEmailMessage(emailMessage);
            return TransformToHtml(xml);
        }

        private string CreateXmlFromEmailMessage(EmailMessage emailMessage)
        {
            using (var stringWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(emailMessage.GetType());
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    ConformanceLevel = ConformanceLevel.Auto
                };

                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    serializer.Serialize(xmlWriter, emailMessage);
                    return stringWriter.ToString();
                }
            }
        }

        private async Task<TokenInfo> GetTokenInfo(Entities.ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString(CultureInfo.InvariantCulture))
            }
            .Union(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"], _configuration["Tokens:Audience"],
                claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);

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

            const string mainMessage = "Please confirm your registration by clicking this link:  ";
            var messageText = Concat(mainMessage, tokenCallbackUrl);
            var htmlMessage = CreateHtmlMessage(userName, tokenCallbackUrl, mainMessage);
            await _emailService.SendEmail(email, fullName, "Email confirmation", messageText, htmlMessage);
        }

        private async Task SendEmailReset(string email, string fullName, Uri tokenCallback, string userName, string resetToken)
        {
            var uriBuilder = new UriBuilder(tokenCallback);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["userName"] = userName;
            query["token"] = resetToken;
            uriBuilder.Query = query.ToString();
            var tokenCallbackUrl = uriBuilder.ToString();

            const string mainMessage = "Please click this link to reset your password:  ";
            var messageText = Concat(mainMessage, tokenCallbackUrl);
            var htmlMessage = CreateHtmlMessage(userName, tokenCallbackUrl, mainMessage);
            await _emailService.SendEmail(email, fullName, "Password reset", messageText, htmlMessage);
        }

        private string TransformToHtml(string xml)
        {
            const string xsltTemplate = @"<?xml version='1.0' ?>
            <xsl:stylesheet xmlns:xsl='http://www.w3.org/1999/XSL/Transform' version='1.0'>
               <xsl:template match='/EmailMessage'>
	                <h2> <xsl:value-of select='UserName'/>!</h2>
                    <p><xsl:value-of select='MainMessage'/><br><xsl:value-of select='CallbackUrl'/></br></p>
               </xsl:template>
            </xsl:stylesheet>";

            //read xml
            using (var xmlReader = XmlReader.Create(new StringReader(xml), new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Auto }))
            using (var stringWriter = new StringWriter())
            using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings { ConformanceLevel = ConformanceLevel.Auto }))
            {
                // load xslt
                var xslt = new XslCompiledTransform();
                xslt.Load(XmlReader.Create(new StringReader(xsltTemplate)));

                // Execute the transform
                xslt.Transform(xmlReader, writer);
                return stringWriter.ToString();
            }
        }
    }
}