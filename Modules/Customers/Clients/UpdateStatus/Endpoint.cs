namespace Deve.Customers.Clients.UpdateStatus;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPatch(CustomersConstants.PathClientV1 + "{id:guid}/" + CustomersConstants.MethodUpdateStatus, async (Guid id, ClientUpdateStatusRequest request, IClientData data, CancellationToken cancellationToken) =>
            await data.UpdateStatusAsync(id, request, cancellationToken))
        .WithTags(CustomersConstants.TagCustomersClient);
}
