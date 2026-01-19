namespace Deve.Customers.Cities.Add;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPost(CustomersConstants.PathCityV1, async (CityAddRequest request, ICityData data, CancellationToken cancellationToken) =>
            await data.AddAsync(request, cancellationToken))
        .WithTags(CustomersConstants.TagCity);
}
