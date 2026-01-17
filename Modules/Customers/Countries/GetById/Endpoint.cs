namespace Deve.Customers.Countries.GetById;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(ApiConstants.PathCountryV1 + "{id:guid}", async (Guid id, ICountryData data, CancellationToken cancellationToken) =>
            await data.GetByIdAsync(id, cancellationToken))
        .WithTags(ApiConstants.TagCustomersCountry);
}
