namespace Deve.Customers.Countries;

public sealed record CountryUpdateRequest
{
    public required string Name { get; init; }
    public required string IsoCode { get; init; }
}
