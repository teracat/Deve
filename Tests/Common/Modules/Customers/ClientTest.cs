using Deve.Data;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;
using Deve.Customers.Clients;
using Deve.Customers.Enums;

namespace Deve.Tests.Modules.Customers;

public abstract class ClientTest : DataAllBaseTest<IData, ClientListResponse, ClientResponse>
{
    #region Constructor
    protected ClientTest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Overrides
    protected override object CreateRequestGetList() => new ClientGetListRequest();

    protected override object CreateInvalidRequestToAdd() => new ClientAddRequest
    {
        Name = "",
        CityId = Guid.Empty,
        Status = ClientStatus.Inactive,
        Balance = 0
    };

    protected override object CreateInvalidRequestToUpdate() => new ClientUpdateRequest
    {
        Name = "",
        CityId = Guid.Empty,
        Status = ClientStatus.Inactive,
        Balance = 0
    };

    protected override object CreateValidRequestToAdd() => new ClientAddRequest
    {
        Name = "Tests Client",
        TradeName = "Tests",
        TaxName = "Tests Client Corporation",
        CityId = TestsConstants.SantpedorCityId,
        Status = ClientStatus.Active,
        Balance = 12
    };

    protected override object CreateValidRequestToUpdate() => new ClientUpdateRequest
    {
        Name = "Jordi Badia",
        TradeName = "Teracat",
        TaxName = "Jordi Badia Santaulària",
        CityId = TestsConstants.SantpedorCityId,
        Status = ClientStatus.Active,
        Balance = 50
    };

    protected override Task<ResultGetList<ClientListResponse>> GetListAsync(IData data, object? request) => data.Customers.Clients.GetAsync((ClientGetListRequest?)request);
    protected override Task<ResultGet<ClientResponse>> GetByIdAsync(IData data, Guid? id) => data.Customers.Clients.GetByIdAsync(id ?? Guid.Empty);

    protected override Task<ResultGet<ResponseId>> AddAsync(IData data, object? request) => data.Customers.Clients.AddAsync((ClientAddRequest)request);
    protected override Task<Result> UpdateAsync(IData data, Guid id, object? request) => data.Customers.Clients.UpdateAsync(id, (ClientUpdateRequest)request);
    protected override Task<Result> DeleteAsync(IData data, Guid id) => data.Customers.Clients.DeleteAsync(id);
    #endregion

    #region Custom Tests
    [Fact]
    public async Task UpdateStatus_NoAuthValidData_ReturnNotSuccess()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Active);

        var res = await Fixture.DataNoAuth.Customers.Clients.UpdateStatusAsync(ValidId, request);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task UpdateStatus_NoAuthValidData_ReturnErrorNotNull()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Active);

        var res = await Fixture.DataNoAuth.Customers.Clients.UpdateStatusAsync(ValidId, request);

        Assert.NotNull(res.Errors);
    }

    [Fact]
    public async Task UpdateStatus_NoAuthValidData_ReturnErrorNotEmpty()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Active);

        var res = await Fixture.DataNoAuth.Customers.Clients.UpdateStatusAsync(ValidId, request);

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task UpdateStatus_NoAuthValidData_ReturnErrorType()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Active);

        var res = await Fixture.DataNoAuth.Customers.Clients.UpdateStatusAsync(ValidId, request);

        _ = Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
    }

    [Fact]
    public async Task UpdateStatus_InvalidData_ReturnNotSuccess()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Active);

        var res = await Fixture.DataAuthAdmin.Customers.Clients.UpdateStatusAsync(Guid.Empty, request);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task UpdateStatus_InvalidData_ReturnErrorsNotNull()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Active);

        var res = await Fixture.DataAuthAdmin.Customers.Clients.UpdateStatusAsync(Guid.Empty, request);

        Assert.NotNull(res.Errors);
    }

    [Fact]
    public async Task UpdateStatus_InvalidData_ReturnErrorsType()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Active);

        var res = await Fixture.DataAuthAdmin.Customers.Clients.UpdateStatusAsync(Guid.Empty, request);

        _ = Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
    }

    [Fact]
    public async Task UpdateStatus_InvalidData_ReturnErrorsNotEmpty()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Active);

        var res = await Fixture.DataAuthAdmin.Customers.Clients.UpdateStatusAsync(Guid.Empty, request);

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task UpdateStatus_ValidData_ReturnSuccess()
    {
        var request = new ClientUpdateStatusRequest(ClientStatus.Active);

        var res = await Fixture.DataAuthAdmin.Customers.Clients.UpdateStatusAsync(ValidId, request);

        Assert.True(res.Success);
    }
    #endregion
}
