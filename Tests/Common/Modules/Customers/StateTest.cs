using Deve.Data;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;
using Deve.Customers.States;

namespace Deve.Tests.Modules.Customers;

public abstract class StateTest : DataAllBaseTest<IData, StateResponse, StateResponse>
{
    #region Constructor
    protected StateTest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Overrides
    protected override object CreateRequestGetList() => new StateGetListRequest();

    protected override object CreateInvalidRequestToAdd() => new StateAddRequest(string.Empty, Guid.Empty);

    protected override object CreateInvalidRequestToUpdate() => new StateUpdateRequest(string.Empty, Guid.Empty);

    protected override object CreateValidRequestToAdd() => new StateAddRequest
    (
        Name: "Tests State",
        CountryId: TestsConstants.SpainCountryId
    );

    protected override object CreateValidRequestToUpdate() => new StateUpdateRequest
    (
        Name: "Barcelona",
        CountryId: TestsConstants.SpainCountryId
    );

    protected override Task<ResultGetList<StateResponse>> GetListAsync(IData data, object? request) => data.Customers.States.GetAsync((StateGetListRequest?)request);
    protected override Task<ResultGet<StateResponse>> GetByIdAsync(IData data, Guid? id) => data.Customers.States.GetByIdAsync(id ?? Guid.Empty);

    protected override Task<ResultGet<ResponseId>> AddAsync(IData data, object? request) => data.Customers.States.AddAsync((StateAddRequest)request);
    protected override Task<Result> UpdateAsync(IData data, Guid id, object? request) => data.Customers.States.UpdateAsync(id, (StateUpdateRequest)request);
    protected override Task<Result> DeleteAsync(IData data, Guid id) => data.Customers.States.DeleteAsync(id);
    #endregion
}
