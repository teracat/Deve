using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;
using Deve.MODULE_NAME;
using Deve.MODULE_NAME.FEATURE_PLURAL;

namespace Deve.Sdk.MODULE_NAME;

internal class FEATURE_SINGULARSdk : BaseSdk, IFEATURE_SINGULARData
{
    private static readonly string Path = MODULE_NAMEConstants.PathFEATURE_SINGULARV1;

    public FEATURE_SINGULARSdk(ISdk sdk)
        : base(sdk)
    {
    }

    // Query
    public async Task<ResultGetList<FEATURE_SINGULARResponse>> GetAsync(FEATURE_SINGULARGetListRequest? request, CancellationToken cancellationToken) => await GetList<FEATURE_SINGULARResponse, FEATURE_SINGULARGetListRequest>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<ResultGet<FEATURE_SINGULARResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await Get<FEATURE_SINGULARResponse>(Path, RequestAuthType.Default, id, cancellationToken);

    // <hooks:sdk-queries>

    // Command
    public async Task<ResultGet<ResponseId>> AddAsync(FEATURE_SINGULARAddRequest request, CancellationToken cancellationToken) => await PostWithResult<ResponseId>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdateAsync(Guid id, FEATURE_SINGULARUpdateRequest request, CancellationToken cancellationToken) => await Put(Path + id, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken) => await Delete(Path + id, RequestAuthType.Default, cancellationToken);

    // <hooks:sdk-commands>
}
