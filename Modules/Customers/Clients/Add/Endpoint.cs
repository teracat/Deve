namespace Deve.Customers.Clients.Add;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPost(CustomersConstants.PathClientV1, async (ClientAddRequest request, IClientData data, CancellationToken cancellationToken) =>
            await data.AddAsync(request, cancellationToken))
        .WithTags(CustomersConstants.TagCustomersClient);
}
