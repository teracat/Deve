using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Deve.Api;

namespace Deve.Tests.Api
{
    public class TestApiFactory<TEntryPoint> where TEntryPoint : class
    {
        private readonly WebApplicationFactory<TEntryPoint> _factory;

        public TestApiFactory(WebApplicationFactory<TEntryPoint> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IDataSourceBuilder, DataSourceBuilderMock>();
                });
            });
        }

        public HttpClient CreateClient() => _factory.CreateClient();
    }
}
