namespace Deve.Customers.Countries.GetList;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(CustomersConstants.PathCountryV1, async ([AsParameters] CountryGetListRequest request, ICountryData data, CancellationToken cancellationToken) =>
            await data.GetAsync(request, cancellationToken))
        .WithTags(CustomersConstants.TagCustomersCountry);
}
