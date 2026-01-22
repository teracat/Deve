using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL;

public interface IITEM_NAME_SINGULARData : IModuleItem
{
    // Queries
    Task<ResultGetList<ITEM_NAME_SINGULARResponse>> GetAsync(ITEM_NAME_SINGULARGetListRequest? request, CancellationToken cancellationToken = default);
    Task<ResultGetList<ITEM_NAME_SINGULARResponse>> GetAsync(CancellationToken cancellationToken = default);
    Task<ResultGet<ITEM_NAME_SINGULARResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    // <hooks:data-queries>

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(ITEM_NAME_SINGULARAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Guid id, ITEM_NAME_SINGULARUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    // <hooks:data-commands>
}
