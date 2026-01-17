namespace Deve.Customers.States.Update;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPut(ApiConstants.PathStateV1 + "{id:guid}", async (Guid id, StateUpdateRequest request, IStateData data, CancellationToken cancellationToken) =>
            await data.UpdateAsync(id, request, cancellationToken))
        .WithTags(ApiConstants.TagCustomersState);
}
