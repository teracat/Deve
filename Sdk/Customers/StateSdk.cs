using Deve.Customers;
using Deve.Customers.States;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Sdk.Customers;

internal class StateSdk : BaseSdk, IStateData
{
    private const string Path = CustomersConstants.PathStateV1;

    public StateSdk(ISdk sdk)
        : base(sdk)
    {
    }

    // Query
    public async Task<ResultGetList<StateResponse>> GetAsync(StateGetListRequest? request, CancellationToken cancellationToken) => await GetList<StateResponse, StateGetListRequest>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<ResultGet<StateResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await Get<StateResponse>(Path, RequestAuthType.Default, id, cancellationToken);

    // <hooks:sdk-queries>

    // Command
    public async Task<ResultGet<ResponseId>> AddAsync(StateAddRequest request, CancellationToken cancellationToken) => await PostWithResult<ResponseId>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdateAsync(Guid id, StateUpdateRequest request, CancellationToken cancellationToken) => await Put(Path + id, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken) => await Delete(Path + id, RequestAuthType.Default, cancellationToken);

    // <hooks:sdk-commands>
}
