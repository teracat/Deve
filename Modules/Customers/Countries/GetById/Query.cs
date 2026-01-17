namespace Deve.Customers.Countries.GetById;

internal sealed record Query(Guid Id) : IRequest<ResultGet<CountryResponse>>;
