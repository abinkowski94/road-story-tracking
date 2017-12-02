using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadStoryTracking.WebApi.AppStartup;
using System.Collections.Generic;

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
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });
            app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
            app.UseStaticFiles();

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            ApplicationInsightsTelemetryConfiguration.ConfigureApplicationInsightsTelemetry(services, Configuration);
            FiltersConfiguration.ConfigureFilter(services);
            DatabaseConfiguration.ConfigureDatabae(services, Configuration);
            IdentityConfiguration.ConfigureIdentity(services);
            JwtConfiguration.ConfigureJwtAuthService(services, Configuration);
            DependencyInjectorConfiguration.ConfigureDependencyInjector(services, Configuration);
        }
    }
}