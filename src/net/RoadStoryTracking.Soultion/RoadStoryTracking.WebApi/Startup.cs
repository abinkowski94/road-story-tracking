using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadStoryTracking.WebApi.AppStartup;

namespace RoadStoryTracking.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());
            app.UseAuthentication();
            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApplicationInsightsTelemetryConfiguration.ConfigureApplicationInsightsTelemetry(services, Configuration);
            FiltersConfiguration.ConfigureFilter(services);
            DatabaseConfiguration.ConfigureDatabae(services, Configuration);
            IdentityConfiguration.ConfigureIdentity(services);
            JwtConfiguration.ConfigureJwtAuthService(services, Configuration);
            DependencyInjectorConfiguration.ConfigureDependencyInjector(services, Configuration);
        }
    }
}