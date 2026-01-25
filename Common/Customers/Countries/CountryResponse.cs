namespace Deve.Customers.Countries;

public sealed record CountryResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string IsoCode { get; init; }
}
