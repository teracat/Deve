using Deve.Dto.Responses.Results;
using Deve.Customers.Stats;

namespace Deve.Sdk.Customers;

internal class StatsSdk : BaseSdk, IStatsData
{
    private const string Path = ApiConstants.PathStatsV1;

    public StatsSdk(ISdk sdk)
        : base(sdk)
    {
    }

    // Query
    public async Task<ResultGet<ClientStatsResponse>> GetClientStatsAsync(CancellationToken cancellationToken = default) => await Get<ClientStatsResponse>(Path + ApiConstants.MethodGetClientStats, RequestAuthType.Default, null, cancellationToken);
}
