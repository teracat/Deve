using Deve.Customers;
using Deve.Customers.Stats;
using Deve.Dto.Responses.Results;

namespace Deve.Sdk.Customers;

internal class StatsSdk : BaseSdk, IStatsData
{
    private static readonly string Path = CustomersConstants.PathStatsV1;

    public StatsSdk(ISdk sdk)
        : base(sdk)
    {
    }

    // Query
    public async Task<ResultGet<ClientStatsResponse>> GetClientStatsAsync(CancellationToken cancellationToken) => await Get<ClientStatsResponse>(Path + CustomersConstants.MethodGetClientStats, RequestAuthType.Default, null, cancellationToken);
}
