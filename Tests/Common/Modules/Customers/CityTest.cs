using Deve.Data;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;
using Deve.Customers.Cities;

namespace Deve.Tests.Modules.Customers;

public abstract class CityTest : DataAllBaseTest<IData, CityResponse, CityResponse>
{
    #region Constructor
    protected CityTest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Overrides
    protected override object CreateRequestGetList() => new CityGetListRequest();

    protected override object CreateInvalidRequestToAdd() => new CityAddRequest(string.Empty, Guid.Empty);

    protected override object CreateInvalidRequestToUpdate() => new CityUpdateRequest(string.Empty, Guid.Empty);

    protected override object CreateValidRequestToAdd() => new CityAddRequest
    (
        Name: "Tests City",
        StateId: TestsConstants.BarcelonaStateId
    );

    protected override object CreateValidRequestToUpdate() => new CityUpdateRequest
    (
        Name: "Santpedor",
        StateId: TestsConstants.BarcelonaStateId
    );

    protected override Task<ResultGetList<CityResponse>> GetListAsync(IData data, object? request) => data.Customers.Cities.GetAsync((CityGetListRequest?)request);
    protected override Task<ResultGet<CityResponse>> GetByIdAsync(IData data, Guid? id) => data.Customers.Cities.GetByIdAsync(id ?? Guid.Empty);

    protected override Task<ResultGet<ResponseId>> AddAsync(IData data, object? request) => data.Customers.Cities.AddAsync((CityAddRequest)request);
    protected override Task<Result> UpdateAsync(IData data, Guid id, object? request) => data.Customers.Cities.UpdateAsync(id, (CityUpdateRequest)request);
    protected override Task<Result> DeleteAsync(IData data, Guid id) => data.Customers.Cities.DeleteAsync(id);
    #endregion

    #region Custom Tests
    [Fact]
    public async Task Get_ValidId_ReturnDataIdMatch()
    {
        var res = await Fixture.DataAuthAdmin.Customers.Cities.GetByIdAsync(ValidId);

        Assert.NotNull(res.Data);
        Assert.Equal(ValidId, res.Data.Id);
    }
    #endregion
}
