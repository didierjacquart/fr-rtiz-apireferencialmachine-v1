using System;
using AxaFrance.ProductMonitoring;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BDM.ReferencialMachine.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>

            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    if (context.HostingEnvironment.IsProduction())
                    {
                        var builtConfig = config.Build();
                        var keyVaultConfigBuilder = new ConfigurationBuilder();
                        keyVaultConfigBuilder.AddAzureKeyVault(new Uri(builtConfig["KeyVault:BaseUrl"]), new DefaultAzureCredential());
                        var keyVaultConfig = keyVaultConfigBuilder.Build();
                        config.AddConfiguration(keyVaultConfig);
                    }

                })
                .ConfigureServices(
                    (context, services) =>
                    {
                        services.AddProductMonitoring(context.Configuration.GetSection("ProductMonitoring").Bind);
                    })
                .ConfigureLogging(
                    (context, builder) =>
                    {
                        builder.AddSerilog(new LoggerConfiguration()
                            .ReadFrom.Configuration(context.Configuration)
                            .CreateLogger());
                    })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseDefaultServiceProvider(options => options.ValidateScopes = false);
                });
    }
}

