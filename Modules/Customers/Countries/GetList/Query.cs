namespace Deve.Customers.Countries.GetList;

internal sealed record Query(Guid? Id, string? Name, string? IsoCode, int? Offset, int? Limit, string? OrderBy) : IRequest<ResultGetList<CountryResponse>>;
