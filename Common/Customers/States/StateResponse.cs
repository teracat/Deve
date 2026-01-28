namespace Deve.Customers.States;

public sealed record StateResponse
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
    public Guid CountryId { get; init; } = Guid.Empty;
    public string? CountryName { get; init; }
}
