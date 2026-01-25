namespace Deve.Customers.States;

public sealed record StateResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required Guid CountryId { get; init; }
    public required string? CountryName { get; init; }
}
