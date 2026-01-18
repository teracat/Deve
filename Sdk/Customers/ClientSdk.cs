using Deve.Customers;
using Deve.Customers.Clients;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Sdk.Customers;

internal class ClientSdk : BaseSdk, IClientData
{
    private const string Path = CustomersConstants.PathClientV1;

    public ClientSdk(ISdk sdk)
        : base(sdk)
    {
    }

    // Query
    public async Task<ResultGetList<ClientListResponse>> GetAsync(ClientGetListRequest? request, CancellationToken cancellationToken = default) => await GetList<ClientListResponse, ClientGetListRequest>(Path, request, RequestAuthType.Default, cancellationToken);
    public async Task<ResultGetList<ClientListResponse>> GetAsync(CancellationToken cancellationToken = default) => await GetAsync(default, cancellationToken);
    public async Task<ResultGet<ClientResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => await Get<ClientResponse>(Path, RequestAuthType.Default, id, cancellationToken);

    // Command
    public async Task<ResultGet<ResponseId>> AddAsync(ClientAddRequest request, CancellationToken cancellationToken = default) => await PostWithResult<ResponseId>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdateAsync(Guid id, ClientUpdateRequest request, CancellationToken cancellationToken = default) => await Put(Path + id, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdateStatusAsync(Guid id, ClientUpdateStatusRequest request, CancellationToken cancellationToken = default) => await Patch(Path + id + ApiConstants.MethodSeparator + CustomersConstants.MethodUpdateStatus, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default) => await Delete(Path + id, RequestAuthType.Default, cancellationToken);
}
