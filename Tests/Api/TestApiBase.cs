using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Deve.DataSource;

namespace Deve.Tests.Api
{
    /// <summary>
    /// Api tests base class.
    /// </summary>
    public class TestApiBase<TEntryPoint> : IClassFixture<WebApplicationFactory<TEntryPoint>> where TEntryPoint : class
    {
        private readonly WebApplicationFactory<TEntryPoint> _factory;

        public TestApiBase(WebApplicationFactory<TEntryPoint> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IDataSourceBuilder, DataSourceBuilderMock>();
                });
            });
        }

        protected HttpClient CreateClient() => _factory.CreateClient();

        protected HttpClient CreateClientWithAuth(string token)
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(ApiConstants.AuthDefaultScheme, token);
            return client;
        }
    }
}