namespace Deve.Customers.Countries.Delete;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapDelete(CustomersConstants.PathCountryV1 + "{id:guid}", async (Guid id, ICountryData data, CancellationToken cancellationToken) =>
            await data.DeleteAsync(id, cancellationToken))
        .WithTags(CustomersConstants.TagCountry);
}
