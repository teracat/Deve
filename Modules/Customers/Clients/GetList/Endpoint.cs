namespace Deve.Customers.Clients.GetList;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(CustomersConstants.PathClientV1, async ([AsParameters] ClientGetListRequest request, IClientData data, CancellationToken cancellationToken) =>
            await data.GetAsync(request, cancellationToken))
        .WithTags(CustomersConstants.TagClient);
}
