namespace Deve.Customers.Countries;

public sealed record CountryGetListRequest
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public string? IsoCode { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
