namespace Deve.Customers.Cities;

public sealed record CityResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid StateId { get; init; }

    public string? StateName { get; init; }
    public string? CountryName { get; init; }
}
