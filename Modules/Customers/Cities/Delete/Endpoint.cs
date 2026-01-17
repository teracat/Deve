namespace Deve.Customers.Cities.Delete;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapDelete(ApiConstants.PathCityV1 + "{id:guid}", async (Guid id, ICityData data, CancellationToken cancellationToken) =>
            await data.DeleteAsync(id, cancellationToken))
        .WithTags(ApiConstants.TagCustomersCity);
}
