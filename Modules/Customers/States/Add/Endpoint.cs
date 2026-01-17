namespace Deve.Customers.States.Add;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPost(ApiConstants.PathStateV1, async (StateAddRequest request, IStateData data, CancellationToken cancellationToken) =>
            await data.AddAsync(request, cancellationToken))
        .WithTags(ApiConstants.TagCustomersState);
}
