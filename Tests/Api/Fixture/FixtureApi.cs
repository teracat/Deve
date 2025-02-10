using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Deve.Api.DataSourceBuilder;

namespace Deve.Tests.Api.Fixture
{
    public class FixtureApi<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint: class
    {
        protected readonly WebApplicationFactory<TEntryPoint> _factory;

        public FixtureApi()
        {
            _factory = WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IDataSourceBuilder, DataSourceBuilderMock>();
                });
            });
        }
    }
}
