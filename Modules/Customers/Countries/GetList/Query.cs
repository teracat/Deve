namespace Deve.Customers.Countries.GetList;

internal sealed record Query : IRequest<ResultGetList<CountryResponse>>
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public string? IsoCode { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
