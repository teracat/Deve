namespace Deve.Customers.Countries.Update;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPut(CustomersConstants.PathCountryV1 + "{id:guid}", async (Guid id, CountryUpdateRequest request, ICountryData data, CancellationToken cancellationToken) =>
            await data.UpdateAsync(id, request, cancellationToken))
        .WithTags(CustomersConstants.TagCountry);
}
