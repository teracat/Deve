namespace Deve.Customers.States.GetList;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(CustomersConstants.PathStateV1, async ([AsParameters] StateGetListRequest request, IStateData data, CancellationToken cancellationToken) =>
            await data.GetAsync(request, cancellationToken))
        .WithTags(CustomersConstants.TagCustomersState);
}
