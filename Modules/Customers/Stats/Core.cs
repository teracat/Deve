namespace Deve.Customers.Stats;

internal sealed class Core(IMediator mediator) : IStatsData
{
    // Queries
    public async Task<ResultGet<ClientStatsResponse>> GetClientStatsAsync(CancellationToken cancellationToken = default)
    {
        var query = new GetClientStats.Query();
        return await mediator.SendAsync(query, cancellationToken);
    }
}
