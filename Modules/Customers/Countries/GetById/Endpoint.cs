namespace Deve.Customers.Countries.GetById;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(CustomersConstants.PathCountryV1 + "{id:guid}", async (Guid id, ICountryData data, CancellationToken cancellationToken) =>
            await data.GetByIdAsync(id, cancellationToken))
        .WithTags(CustomersConstants.TagCountry);
}
