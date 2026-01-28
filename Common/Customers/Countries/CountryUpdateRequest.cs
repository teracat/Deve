namespace Deve.Customers.Countries;

public sealed record CountryUpdateRequest
{
    public string Name { get; init; } = string.Empty;
    public string IsoCode { get; init; } = string.Empty;
}
