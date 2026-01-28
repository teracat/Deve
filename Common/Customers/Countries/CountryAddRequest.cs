namespace Deve.Customers.Countries;

public sealed record CountryAddRequest
{
    public string Name { get; init; } = string.Empty;
    public string IsoCode { get; init; } = string.Empty;
}
