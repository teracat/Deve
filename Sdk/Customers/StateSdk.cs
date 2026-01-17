using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;
using Deve.Customers.States;

namespace Deve.Sdk.Customers;

internal class StateSdk : BaseSdk, IStateData
{
    private const string Path = ApiConstants.PathStateV1;

    public StateSdk(ISdk sdk)
        : base(sdk)
    {
    }

    // Query
    public async Task<ResultGetList<StateResponse>> GetAsync(StateGetListRequest? request, CancellationToken cancellationToken = default) => await GetList<StateResponse, StateGetListRequest>(Path, request, RequestAuthType.Default, cancellationToken);
    public async Task<ResultGetList<StateResponse>> GetAsync(CancellationToken cancellationToken = default) => await GetAsync(default, cancellationToken);
    public async Task<ResultGet<StateResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => await Get<StateResponse>(Path, RequestAuthType.Default, id, cancellationToken);

    // Command
    public async Task<ResultGet<ResponseId>> AddAsync(StateAddRequest request, CancellationToken cancellationToken = default) => await PostWithResult<ResponseId>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdateAsync(Guid id, StateUpdateRequest request, CancellationToken cancellationToken = default) => await Put(Path + id, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default) => await Delete(Path + id, RequestAuthType.Default, cancellationToken);
}
