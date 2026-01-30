namespace Deve.Customers.Cities;

public sealed record CityAddRequest
{
    public string Name { get; init; } = string.Empty;
    public Guid StateId { get; init; } = Guid.Empty;
}
