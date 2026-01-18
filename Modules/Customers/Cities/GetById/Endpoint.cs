namespace Deve.Customers.Cities.GetById;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(CustomersConstants.PathCityV1 + "{id:guid}", async (Guid id, ICityData data, CancellationToken cancellationToken) =>
            await data.GetByIdAsync(id, cancellationToken))
        .WithTags(CustomersConstants.TagCustomersCity);
}
