using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RoadStoryTracking.WebApi.Data.Context;
using RoadStoryTracking.WebApi.Data.Models;
using System;

namespace RoadStoryTracking.WebApi.AppStartup
{
    public static class IdentityConfiguration
    {
        public static void ConfigureIdentity(IServiceCollection services)
        {
            var dataProtectionProviderType = typeof(DataProtectorTokenProvider<ApplicationUser>);
            var phoneNumberProviderType = typeof(PhoneNumberTokenProvider<ApplicationUser>);
            var emailTokenProviderType = typeof(EmailTokenProvider<ApplicationUser>);

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Lockout = new LockoutOptions
                {
                    DefaultLockoutTimeSpan = TimeSpan.FromHours(1),
                    MaxFailedAccessAttempts = 5
                };
            }).AddEntityFrameworkStores<RoadStoryTrackingDbContext>()
            .AddTokenProvider(TokenOptions.DefaultProvider, dataProtectionProviderType)
            .AddTokenProvider(TokenOptions.DefaultEmailProvider, emailTokenProviderType)
            .AddTokenProvider(TokenOptions.DefaultPhoneProvider, phoneNumberProviderType);
        }
    }
}