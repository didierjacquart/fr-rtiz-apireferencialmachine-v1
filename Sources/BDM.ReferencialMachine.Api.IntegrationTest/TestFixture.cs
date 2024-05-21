using System;
using BDM.ReferencialMachine.DataAccess.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace BDM.ReferencialMachine.Api.IntegrationTest
{

    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        private MachineContext _context { get; set; }

        public WebApplicationFactory<TStartup> WebApplicationFactory { get; private set; }

        public TestFixture()
        {
            WebApplicationFactory = ConfigureWebApplicationFactory();
        }

        private WebApplicationFactory<TStartup> ConfigureWebApplicationFactory(Action<IServiceCollection> configureTestServices = null)
        {
            var options = new DbContextOptionsBuilder<MachineContext>()
                .UseInMemoryDatabase(databaseName:Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;
            _context = new MachineContext(options);

            return new WebApplicationFactory<TStartup>().WithWebHostBuilder(builder =>
            {
                // Before TStartup ConfigureServices.
                builder.ConfigureServices(services => { });
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(serviceProvider => _context);
                });
            });
        }

        public void Dispose()
        {
            _context?.Dispose();
            WebApplicationFactory?.Dispose();
        }
    }
}
