using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Deve.Auth.TokenManagers;
using Deve.Api.DataSourceBuilder;

namespace Deve.Tests.Api.Fixture
{
    public class FixtureApi<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint: class
    {
        protected readonly WebApplicationFactory<TEntryPoint> _factory;
        private readonly ITokenManager _tokenManager;

        public FixtureApi()
        {
            _tokenManager = TestsHelpers.CreateTokenManager();
            _factory = WithWebHostBuilder(builder =>
            {
                builder.UseConfiguration(new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build());
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IDataSourceBuilder, DataSourceBuilderMock>();
                    services.AddSingleton(_tokenManager);
                });
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _tokenManager.Dispose();
        }
    }
}