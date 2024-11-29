using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Deve.Api;
using Deve.Internal.Sdk;
using Deve.Tests.Api;
using Program = Deve.Internal.Api.Program;

namespace Deve.Tests.Sdk.Internal
{
    public class FixtureDataSdk : WebApplicationFactory<Program>, IFixtureData<ISdk>
    {
        public ISdk Data { get; private set; }
        
        private readonly WebApplicationFactory<Program> _factory;

        public FixtureDataSdk()
        {
            _factory = WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<IDataSourceBuilder, DataSourceBuilderMock>();
                });
            });

            Data = new SdkMain(_factory.CreateClient());
        }
    }
}
