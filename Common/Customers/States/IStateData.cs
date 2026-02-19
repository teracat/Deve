using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Customers.States;

public interface IStateData : IFeature
{
    // Queries
    Task<ResultGetList<StateResponse>> GetAsync() => GetAsync(null, CancellationToken.None);
    Task<ResultGetList<StateResponse>> GetAsync(CancellationToken cancellationToken) => GetAsync(null, cancellationToken);
    Task<ResultGetList<StateResponse>> GetAsync(StateGetListRequest? request) => GetAsync(request, CancellationToken.None);
    Task<ResultGetList<StateResponse>> GetAsync(StateGetListRequest? request, CancellationToken cancellationToken);

    Task<ResultGet<StateResponse>> GetByIdAsync(Guid id) => GetByIdAsync(id, CancellationToken.None);
    Task<ResultGet<StateResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-queries>

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(StateAddRequest request) => AddAsync(request, CancellationToken.None);
    Task<ResultGet<ResponseId>> AddAsync(StateAddRequest request, CancellationToken cancellationToken);

    Task<Result> UpdateAsync(Guid id, StateUpdateRequest request) => UpdateAsync(id, request, CancellationToken.None);
    Task<Result> UpdateAsync(Guid id, StateUpdateRequest request, CancellationToken cancellationToken);

    Task<Result> DeleteAsync(Guid id) => DeleteAsync(id, CancellationToken.None);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-commands>
}
