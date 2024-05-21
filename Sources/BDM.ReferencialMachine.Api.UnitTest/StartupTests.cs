using System;
using System.Collections.Generic;
using BDM.ReferencialMachine.Api.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace BDM.ReferencialMachine.Api.UnitTest
{
    public class StartupTests
    {
        [Fact]
        public void Should_Nominal_CreateServiceProvider()
        {
            var exceptions = new List<Exception>();
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddJsonFile("appsettings.json");
                    configurationBuilder.AddJsonFile("appsettings.Development.json");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseDefaultServiceProvider(options => options.ValidateScopes = false);

                }).Build();

            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();

            var services = new ServiceCollection();

            var startup = new Startup(configuration);

            startup.ConfigureServices(services);

            foreach (var serviceDescriptor in services)
            {
                var serviceType = serviceDescriptor.ServiceType;
                if (!serviceType.FullName.Contains("BDM.ReferencialMachine"))
                {
                    continue;
                }

                var ex = Record.Exception(() => host.Services.GetService(serviceType));
                if (ex != null)
                {
                    exceptions.Add(ex);
                }
            }

            var controllerEx = Record.Exception(() => host.Services.GetService(typeof(MachineController)));
            if (controllerEx != null)
            {
                exceptions.Add(controllerEx);
            }

            //Assert
            Assert.Empty(exceptions);
        }
    }
}
