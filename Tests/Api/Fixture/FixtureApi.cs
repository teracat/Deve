using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Deve.Data;

namespace Deve.Tests.Api.Fixture
{
    public class FixtureApi<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint: class
    {
        protected readonly WebApplicationFactory<TEntryPoint> _factory;

        public FixtureApi()
        {
            _factory = WithWebHostBuilder(builder =>
            {
                builder.UseConfiguration(new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build());
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IDataOptions, DataOptions>();
                    services.AddScoped(provider => TestsHelpers.CreateDataSourceMock());
                    services.AddScoped(provider => TestsHelpers.CreateTokenManager());
                });
            });
        }
    }
}