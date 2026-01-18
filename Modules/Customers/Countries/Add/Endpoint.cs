namespace Deve.Customers.Countries.Add;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPost(CustomersConstants.PathCountryV1, async (CountryAddRequest request, ICountryData data, CancellationToken cancellationToken) =>
            await data.AddAsync(request, cancellationToken))
        .WithTags(CustomersConstants.TagCustomersCountry);
}
