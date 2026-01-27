using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Deve.Api;
using Deve.Data;

namespace Deve.Tests.Api.Fixture;

public class FixtureApi : WebApplicationFactory<Program>
{
    private readonly WebApplicationFactory<Program> _factory;

    public FixtureApi()
    {
        _factory = WithWebHostBuilder(builder =>
        {
            _ = builder.UseConfiguration(new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build());
            _ = builder.ConfigureServices(services =>
            {
                _ = services.AddScoped<IDataOptions, DataOptions>();
                _ = services.AddScoped(_ => TestsHelpers.CreateTokenManager());
                _ = services.AddTests();
            });
        });
    }

    public new HttpClient CreateClient() => _factory.CreateClient();

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _factory.Dispose();
        }
        base.Dispose(disposing);
    }
}
