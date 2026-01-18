namespace Deve.Customers.Stats.GetClientStats;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(CustomersConstants.PathStatsV1 + CustomersConstants.MethodGetClientStats, async (IStatsData data, CancellationToken cancellationToken) =>
            await data.GetClientStatsAsync(cancellationToken))
        .WithTags(CustomersConstants.TagCustomersStats);
}
