using Deve.Data;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;
using Deve.MODULE_NAME.ITEM_NAME_PLURAL;

namespace Deve.Tests.Modules.MODULE_NAME;

public abstract class ITEM_NAME_SINGULARTest : DataAllBaseTest<IData, ITEM_NAME_SINGULARResponse, ITEM_NAME_SINGULARResponse>
{
    #region Constructor
    protected ITEM_NAME_SINGULARTest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Overrides
    protected override object CreateRequestGetList() => ITEM_NAME_SINGULARGetListRequest.Create();

    protected override object CreateInvalidRequestToAdd() => new ITEM_NAME_SINGULARAddRequest("");

    protected override object CreateInvalidRequestToUpdate() => new ITEM_NAME_SINGULARUpdateRequest("");

    protected override object CreateValidRequestToAdd() => new ITEM_NAME_SINGULARAddRequest(
        Name: "Add Tests"
    );

    protected override object CreateValidRequestToUpdate() => new ITEM_NAME_SINGULARUpdateRequest(
        Name: "Update Test"
    );

    protected override Task<ResultGetList<ITEM_NAME_SINGULARResponse>> GetListAsync(IData data, object? request) => data.MODULE_NAME.ITEM_NAME_PLURAL.GetAsync((ITEM_NAME_SINGULARGetListRequest?)request);
    protected override Task<ResultGet<ITEM_NAME_SINGULARResponse>> GetByIdAsync(IData data, Guid? id) => data.MODULE_NAME.ITEM_NAME_PLURAL.GetByIdAsync(id ?? Guid.Empty);

    protected override Task<ResultGet<ResponseId>> AddAsync(IData data, object? request) => data.MODULE_NAME.ITEM_NAME_PLURAL.AddAsync((ITEM_NAME_SINGULARAddRequest)request);
    protected override Task<Result> UpdateAsync(IData data, Guid id, object? request) => data.MODULE_NAME.ITEM_NAME_PLURAL.UpdateAsync(id, (ITEM_NAME_SINGULARUpdateRequest)request);
    protected override Task<Result> DeleteAsync(IData data, Guid id) => data.MODULE_NAME.ITEM_NAME_PLURAL.DeleteAsync(id);
    #endregion
}
