using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Customers.Clients;

public interface IClientData : IModuleItem
{
    // Queries
    Task<ResultGetList<ClientListResponse>> GetAsync(ClientGetListRequest? request, CancellationToken cancellationToken = default);
    Task<ResultGetList<ClientListResponse>> GetAsync(CancellationToken cancellationToken = default);
    Task<ResultGet<ClientResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(ClientAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Guid id, ClientUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateStatusAsync(Guid id, ClientUpdateStatusRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
