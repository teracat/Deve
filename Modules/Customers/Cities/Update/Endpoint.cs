namespace Deve.Customers.Cities.Update;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPut(CustomersConstants.PathCityV1 + "{id:guid}", async (Guid id, CityUpdateRequest request, ICityData data, CancellationToken cancellationToken) =>
            await data.UpdateAsync(id, request, cancellationToken))
        .WithTags(CustomersConstants.TagCity);
}
