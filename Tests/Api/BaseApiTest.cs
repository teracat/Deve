using System.Text;
using System.Text.Json;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api;

/// <summary>
/// Api tests base class.
/// </summary>
public abstract class BaseApiTest
{
    protected FixtureApiClients Fixture { get; }

    protected BaseApiTest(FixtureApiClients fixture)
    {
        Fixture = fixture;
    }

    protected static HttpContent ToHttpContent(object data)
    {
        var json = JsonSerializer.Serialize(data);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
