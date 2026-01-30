namespace Deve.Customers.Clients.Delete;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapDelete(CustomersConstants.PathClientV1 + "{id:guid}", async (Guid id, IClientData data, CancellationToken cancellationToken) =>
            await data.DeleteAsync(id, cancellationToken))
        .WithTags(CustomersConstants.TagClient);
}
