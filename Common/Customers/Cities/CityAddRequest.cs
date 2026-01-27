namespace Deve.Customers.Cities;

public sealed record CityAddRequest
{
    public required string Name { get; init; }
    public required Guid StateId { get; init; }
}
