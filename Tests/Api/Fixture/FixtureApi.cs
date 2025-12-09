using Deve.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Deve.Tests.Api.Fixture
{
    public class FixtureApi<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected readonly WebApplicationFactory<TEntryPoint> _factory;

        public FixtureApi()
        {
            _factory = WithWebHostBuilder(builder =>
            {
                _ = builder.UseConfiguration(new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build());
                _ = builder.ConfigureServices(services =>
                {
                    _ = services.AddScoped<IDataOptions, DataOptions>();
                    _ = services.AddScoped(provider => TestsHelpers.CreateDataSourceMock());
                    _ = services.AddScoped(provider => TestsHelpers.CreateTokenManager());
                });
            });
        }
    }
}