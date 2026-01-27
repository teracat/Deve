namespace Deve.Customers.Countries;

public sealed record CountryAddRequest
{
    public required string Name { get; init; }
    public required string IsoCode { get; init; }
}
