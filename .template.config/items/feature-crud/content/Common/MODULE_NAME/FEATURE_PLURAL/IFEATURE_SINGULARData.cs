using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.MODULE_NAME.FEATURE_PLURAL;

public interface IFEATURE_SINGULARData : IFeature
{
    // Queries
    Task<ResultGetList<FEATURE_SINGULARResponse>> GetAsync() => GetAsync(null, CancellationToken.None);
    Task<ResultGetList<FEATURE_SINGULARResponse>> GetAsync(CancellationToken cancellationToken) => GetAsync(null, cancellationToken);
    Task<ResultGetList<FEATURE_SINGULARResponse>> GetAsync(FEATURE_SINGULARGetListRequest? request) => GetAsync(request, CancellationToken.None);
    Task<ResultGetList<FEATURE_SINGULARResponse>> GetAsync(FEATURE_SINGULARGetListRequest? request, CancellationToken cancellationToken);

    Task<ResultGet<FEATURE_SINGULARResponse>> GetByIdAsync(Guid id) => GetByIdAsync(id, CancellationToken.None);
    Task<ResultGet<FEATURE_SINGULARResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-queries>

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(FEATURE_SINGULARAddRequest request) => AddAsync(request, CancellationToken.None);
    Task<ResultGet<ResponseId>> AddAsync(FEATURE_SINGULARAddRequest request, CancellationToken cancellationToken);

    Task<Result> UpdateAsync(Guid id, FEATURE_SINGULARUpdateRequest request) => UpdateAsync(id, request, CancellationToken.None);
    Task<Result> UpdateAsync(Guid id, FEATURE_SINGULARUpdateRequest request, CancellationToken cancellationToken);

    Task<Result> DeleteAsync(Guid id) => DeleteAsync(id, CancellationToken.None);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-commands>
}
