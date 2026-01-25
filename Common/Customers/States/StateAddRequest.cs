namespace Deve.Customers.States;

public sealed record StateAddRequest
{
    public required string Name { get; init; }
    public required Guid CountryId { get; init; }
}
