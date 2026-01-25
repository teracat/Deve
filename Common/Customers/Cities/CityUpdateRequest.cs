namespace Deve.Customers.Cities;

public sealed record CityUpdateRequest
{
    public required string Name { get; init; }
    public required Guid StateId { get; init; }
}
