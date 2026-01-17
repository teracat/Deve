namespace Deve.Customers.States.GetById;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(ApiConstants.PathStateV1 + "{id:guid}", async (Guid id, IStateData data, CancellationToken cancellationToken) =>
            await data.GetByIdAsync(id, cancellationToken))
        .WithTags(ApiConstants.TagCustomersState);
}
