namespace Deve.Customers.Cities.GetById;

internal sealed record Query(Guid Id) : IRequest<ResultGet<CityResponse>>;
