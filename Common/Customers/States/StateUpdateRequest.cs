namespace Deve.Customers.States;

public sealed record StateUpdateRequest
{
    public string Name { get; init; } = string.Empty;
    public Guid CountryId { get; init; } = Guid.Empty;
}
