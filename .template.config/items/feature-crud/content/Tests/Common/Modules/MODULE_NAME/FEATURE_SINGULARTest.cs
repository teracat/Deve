using Deve.Data;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;
using Deve.MODULE_NAME.FEATURE_PLURAL;

namespace Deve.Tests.Modules.MODULE_NAME;

public abstract class FEATURE_SINGULARTest : DataAllBaseTest<IData, FEATURE_SINGULARResponse, FEATURE_SINGULARResponse>
{
    #region Constructor
    protected FEATURE_SINGULARTest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Overrides
    protected override object CreateRequestGetList() => new FEATURE_SINGULARGetListRequest();

    protected override object CreateInvalidRequestToAdd() => new FEATURE_SINGULARAddRequest { Name = "" };

    protected override object CreateInvalidRequestToUpdate() => new FEATURE_SINGULARUpdateRequest { Name = "" };

    protected override object CreateValidRequestToAdd() => new FEATURE_SINGULARAddRequest
    {
        Name = "Add Tests"
    };

    protected override object CreateValidRequestToUpdate() => new FEATURE_SINGULARUpdateRequest
    {
        Name = "Update Test"
    };

    protected override Task<ResultGetList<FEATURE_SINGULARResponse>> GetListAsync(IData data, object? request) => data.MODULE_NAME.FEATURE_PLURAL.GetAsync((FEATURE_SINGULARGetListRequest?)request);
    protected override Task<ResultGet<FEATURE_SINGULARResponse>> GetByIdAsync(IData data, Guid? id) => data.MODULE_NAME.FEATURE_PLURAL.GetByIdAsync(id ?? Guid.Empty);

    protected override Task<ResultGet<ResponseId>> AddAsync(IData data, object? request) => data.MODULE_NAME.FEATURE_PLURAL.AddAsync((FEATURE_SINGULARAddRequest)request);
    protected override Task<Result> UpdateAsync(IData data, Guid id, object? request) => data.MODULE_NAME.FEATURE_PLURAL.UpdateAsync(id, (FEATURE_SINGULARUpdateRequest)request);
    protected override Task<Result> DeleteAsync(IData data, Guid id) => data.MODULE_NAME.FEATURE_PLURAL.DeleteAsync(id);
    #endregion
}
