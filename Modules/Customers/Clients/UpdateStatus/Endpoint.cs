namespace Deve.Customers.Clients.UpdateStatus;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPatch(ApiConstants.PathClientV1 + "{id:guid}/" + ApiConstants.MethodUpdateStatus, async (Guid id, ClientUpdateStatusRequest request, IClientData data, CancellationToken cancellationToken) =>
            await data.UpdateStatusAsync(id, request, cancellationToken))
        .WithTags(ApiConstants.TagCustomersClient);
}
