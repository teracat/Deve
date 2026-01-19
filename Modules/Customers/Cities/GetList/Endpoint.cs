namespace Deve.Customers.Cities.GetList;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(CustomersConstants.PathCityV1, async ([AsParameters] CityGetListRequest request, ICityData data, CancellationToken cancellationToken) =>
            await data.GetAsync(request, cancellationToken))
        .WithTags(CustomersConstants.TagCity);
}
