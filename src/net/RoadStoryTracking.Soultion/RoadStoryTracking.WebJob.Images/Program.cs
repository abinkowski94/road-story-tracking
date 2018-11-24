using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RoadStoryTracking.WebJob.Images.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebJob.Images
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                MainAsync(args).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
            }
        }

        private static async Task MainAsync(string[] args)
        {
#if DEBUG
            var environment = "Development";
#else
            var environment = "Production";
#endif

            var builder = new HostBuilder()
                .UseEnvironment(environment)
                .ConfigureAppConfiguration(b =>
                {
                    b.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureWebJobs(webJobOptions =>
                {
                    webJobOptions
                    .AddAzureStorageCoreServices()
                    .AddAzureStorage();
                })
                .ConfigureLogging((context, b) =>
                {
                    b.SetMinimumLevel(LogLevel.Debug);
                })
                .UseConsoleLifetime()
                .ConfigureServices((context, services) =>
                 {
                     services.AddTransient<IImageService, ImageService>();
                 });

            var host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}