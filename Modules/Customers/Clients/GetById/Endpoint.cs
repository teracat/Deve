namespace Deve.Customers.Clients.GetById;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(CustomersConstants.PathClientV1 + "{id:guid}", async (Guid id, IClientData data, CancellationToken cancellationToken) =>
            await data.GetByIdAsync(id, cancellationToken))
        .WithTags(CustomersConstants.TagClient);
}
