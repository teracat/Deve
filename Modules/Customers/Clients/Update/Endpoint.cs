namespace Deve.Customers.Clients.Update;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPut(CustomersConstants.PathClientV1 + "{id:guid}", async (Guid id, ClientUpdateRequest request, IClientData data, CancellationToken cancellationToken) =>
            await data.UpdateAsync(id, request, cancellationToken))
        .WithTags(CustomersConstants.TagCustomersClient);
}
