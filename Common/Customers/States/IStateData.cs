using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Customers.States;

public interface IStateData : IModuleItem
{
    // Queries
    Task<ResultGetList<StateResponse>> GetAsync(StateGetListRequest? request, CancellationToken cancellationToken = default);
    Task<ResultGetList<StateResponse>> GetAsync(CancellationToken cancellationToken = default);
    Task<ResultGet<StateResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    // <hooks:data-queries>

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(StateAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Guid id, StateUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    // <hooks:data-commands>
}
