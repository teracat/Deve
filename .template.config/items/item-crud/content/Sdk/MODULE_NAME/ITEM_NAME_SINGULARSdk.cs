using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;
using Deve.MODULE_NAME;
using Deve.MODULE_NAME.ITEM_NAME_PLURAL;

namespace Deve.Sdk.MODULE_NAME;

internal class ITEM_NAME_SINGULARSdk : BaseSdk, IITEM_NAME_SINGULARData
{
    private const string Path = MODULE_NAMEConstants.PathITEM_NAME_SINGULARV1;

    public ITEM_NAME_SINGULARSdk(ISdk sdk)
        : base(sdk)
    {
    }

    // Query
    public async Task<ResultGetList<ITEM_NAME_SINGULARResponse>> GetAsync(ITEM_NAME_SINGULARGetListRequest? request, CancellationToken cancellationToken = default) => await GetList<ITEM_NAME_SINGULARResponse, ITEM_NAME_SINGULARGetListRequest>(Path, request, RequestAuthType.Default, cancellationToken);
    public async Task<ResultGetList<ITEM_NAME_SINGULARResponse>> GetAsync(CancellationToken cancellationToken = default) => await GetAsync(default, cancellationToken);
    public async Task<ResultGet<ITEM_NAME_SINGULARResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => await Get<ITEM_NAME_SINGULARResponse>(Path, RequestAuthType.Default, id, cancellationToken);
    
    // <hooks:sdk-queries>

    // Command
    public async Task<ResultGet<ResponseId>> AddAsync(ITEM_NAME_SINGULARAddRequest request, CancellationToken cancellationToken = default) => await PostWithResult<ResponseId>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdateAsync(Guid id, ITEM_NAME_SINGULARUpdateRequest request, CancellationToken cancellationToken = default) => await Put(Path + id, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default) => await Delete(Path + id, RequestAuthType.Default, cancellationToken);
    
    // <hooks:sdk-commands>
}
