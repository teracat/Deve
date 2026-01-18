using Deve.Customers;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api.Modules.Customers;

public class StatsApiTest : BaseApiTest, IClassFixture<FixtureApiClients>
{
    public StatsApiTest(FixtureApiClients fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task GetClientStats_Unauthorized_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientNoAuth.GetAsync(CustomersConstants.PathStatsV1 + CustomersConstants.MethodGetClientStats);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetClientStats_Valid_SuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.GetAsync(CustomersConstants.PathStatsV1 + CustomersConstants.MethodGetClientStats);

        Assert.True(response.IsSuccessStatusCode);
    }
}
