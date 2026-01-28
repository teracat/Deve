namespace Deve.Customers.Countries;

public sealed record CountryResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string IsoCode { get; init; } = string.Empty;
}
