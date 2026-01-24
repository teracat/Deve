using Deve.Dto.Responses.Results;

namespace Deve.Customers.Stats;

public interface IStatsData : IModuleItem
{
    // Queries
    Task<ResultGet<ClientStatsResponse>> GetClientStatsAsync() => GetClientStatsAsync(CancellationToken.None);
    Task<ResultGet<ClientStatsResponse>> GetClientStatsAsync(CancellationToken cancellationToken);
}
