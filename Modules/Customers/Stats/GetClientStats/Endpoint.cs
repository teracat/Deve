namespace Deve.Customers.Stats.GetClientStats;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(ApiConstants.PathStatsV1 + ApiConstants.MethodGetClientStats, async (IStatsData data, CancellationToken cancellationToken) =>
            await data.GetClientStatsAsync(cancellationToken))
        .WithTags(ApiConstants.TagCustomersStats);
}
