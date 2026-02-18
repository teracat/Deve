using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Customers.Clients;

public interface IClientData : IFeature
{
    // Queries
    Task<ResultGetList<ClientListResponse>> GetAsync() => GetAsync(null, CancellationToken.None);
    Task<ResultGetList<ClientListResponse>> GetAsync(CancellationToken cancellationToken) => GetAsync(null, cancellationToken);
    Task<ResultGetList<ClientListResponse>> GetAsync(ClientGetListRequest? request) => GetAsync(request, CancellationToken.None);
    Task<ResultGetList<ClientListResponse>> GetAsync(ClientGetListRequest? request, CancellationToken cancellationToken);

    Task<ResultGet<ClientResponse>> GetByIdAsync(Guid id) => GetByIdAsync(id, CancellationToken.None);
    Task<ResultGet<ClientResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-queries>

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(ClientAddRequest request) => AddAsync(request, CancellationToken.None);
    Task<ResultGet<ResponseId>> AddAsync(ClientAddRequest request, CancellationToken cancellationToken);

    Task<Result> UpdateAsync(Guid id, ClientUpdateRequest request) => UpdateAsync(id, request, CancellationToken.None);
    Task<Result> UpdateAsync(Guid id, ClientUpdateRequest request, CancellationToken cancellationToken);

    Task<Result> UpdateStatusAsync(Guid id, ClientUpdateStatusRequest request) => UpdateStatusAsync(id, request, CancellationToken.None);
    Task<Result> UpdateStatusAsync(Guid id, ClientUpdateStatusRequest request, CancellationToken cancellationToken);

    Task<Result> DeleteAsync(Guid id) => DeleteAsync(id, CancellationToken.None);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-commands>
}
