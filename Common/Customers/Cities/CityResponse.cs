namespace Deve.Customers.Cities;

public sealed record CityResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required Guid StateId { get; init; }

    public string? StateName { get; init; }
    public string? CountryName { get; init; }
}
