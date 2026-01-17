namespace Deve.Customers.Cities.GetList;

internal sealed record Query(Guid? Id, string? Name, Guid? StateId, int? Offset, int? Limit, string? OrderBy) : IRequest<ResultGetList<CityResponse>>;
