namespace Deve.Customers.States;

public sealed record StateUpdateRequest
{
    public required string Name { get; init; }
    public required Guid CountryId { get; init; }
}
