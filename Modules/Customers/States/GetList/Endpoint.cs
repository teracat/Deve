namespace Deve.Customers.States.GetList;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(ApiConstants.PathStateV1, async ([AsParameters] StateGetListRequest request, IStateData data, CancellationToken cancellationToken) =>
            await data.GetAsync(request, cancellationToken))
        .WithTags(ApiConstants.TagCustomersState);
}
