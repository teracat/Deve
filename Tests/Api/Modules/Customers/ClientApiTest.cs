using Deve.Customers.Clients;
using Deve.Customers.Enums;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api.Modules.Customers;

public class ClientApiTest : BaseAllApiTest, IClassFixture<FixtureApiClients>
{
    protected override string Path => ApiConstants.PathClientV1;

    public ClientApiTest(FixtureApiClients fixture)
        : base(fixture)
    {
    }

    protected override object CreateInvalidRequestToAdd() => new ClientAddRequest
    (
        Name: string.Empty,
        TradeName: null,
        TaxId: null,
        TaxName: null,
        CityId: Guid.Empty,
        Status: ClientStatus.Active,
        Balance: 0
     );

    protected override object CreateInvalidRequestToUpdate() => new ClientUpdateRequest
    (
        Name: string.Empty,
        TradeName: null,
        TaxId: null,
        TaxName: null,
        CityId: Guid.Empty,
        Status: ClientStatus.Active,
        Balance: 0
     );

    protected override object CreateValidRequestToAdd() => new ClientAddRequest
    (
        Name: "Tests Client",
        TradeName: "Tests",
        TaxId: null,
        TaxName: "Tests Client Corporation",
        CityId: TestsConstants.SantpedorCityId,
        Status: ClientStatus.Active,
        Balance: 12
    );

    protected override object CreateValidRequestToUpdate() => new ClientUpdateRequest
    (
        Name: "Jordi Badia",
        TradeName: "Teracat",
        TaxId: null,
        TaxName: "Jordi Badia Santaulària",
        CityId: TestsConstants.SantpedorCityId,
        Status: ClientStatus.Active,
        Balance: 50
    );

    #region UpdateStatus Tests
    [Fact]
    public async Task UpdateStatus_Unauthorized_NotSuccessStatusCode()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Inactive);

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientNoAuth.PutAsync(Path + $"{Guid.Empty}/" + ApiConstants.MethodUpdateStatus, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdateStatus_NullRequest_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.PutAsync(Path + $"{ValidId}/" + ApiConstants.MethodUpdateStatus, null);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdateStatus_EmptyId_NotSuccessStatusCode()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Inactive);

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientAuthAdmin.PutAsync(Path + $"{Guid.Empty}/" + ApiConstants.MethodUpdateStatus, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdateStatus_InvalidId_NotSuccessStatusCode()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Inactive);

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientAuthAdmin.PutAsync(Path + $"{InvalidId}/" + ApiConstants.MethodUpdateStatus, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdateStatus_Valid_SuccessStatusCode()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Inactive);

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientAuthAdmin.PatchAsync(Path + $"{ValidId}/" + ApiConstants.MethodUpdateStatus, httpContent);

        Assert.True(response.IsSuccessStatusCode);
    }
    #endregion
}
