using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL;

public interface IITEM_NAME_SINGULARData : IModuleItem
{
    // Queries
    Task<ResultGetList<ITEM_NAME_SINGULARResponse>> GetAsync() => GetAsync(null, CancellationToken.None);
    Task<ResultGetList<ITEM_NAME_SINGULARResponse>> GetAsync(CancellationToken cancellationToken) => GetAsync(null, cancellationToken);
    Task<ResultGetList<ITEM_NAME_SINGULARResponse>> GetAsync(ITEM_NAME_SINGULARGetListRequest? request) => GetAsync(request, CancellationToken.None);
    Task<ResultGetList<ITEM_NAME_SINGULARResponse>> GetAsync(ITEM_NAME_SINGULARGetListRequest? request, CancellationToken cancellationToken);

    Task<ResultGet<ITEM_NAME_SINGULARResponse>> GetByIdAsync(Guid id) => GetByIdAsync(id, CancellationToken.None);
    Task<ResultGet<ITEM_NAME_SINGULARResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-queries>

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(ITEM_NAME_SINGULARAddRequest request) => AddAsync(request, CancellationToken.None);
    Task<ResultGet<ResponseId>> AddAsync(ITEM_NAME_SINGULARAddRequest request, CancellationToken cancellationToken);

    Task<Result> UpdateAsync(Guid id, ITEM_NAME_SINGULARUpdateRequest request) => UpdateAsync(id, request, CancellationToken.None);
    Task<Result> UpdateAsync(Guid id, ITEM_NAME_SINGULARUpdateRequest request, CancellationToken cancellationToken);

    Task<Result> DeleteAsync(Guid id) => DeleteAsync(id, CancellationToken.None);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-commands>
}
