namespace Deve.Customers.States.Delete;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapDelete(ApiConstants.PathStateV1 + "{id:guid}", async (Guid id, IStateData data, CancellationToken cancellationToken) =>
            await data.DeleteAsync(id, cancellationToken))
        .WithTags(ApiConstants.TagCustomersState);
}
