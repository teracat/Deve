using Deve.Customers.Enums;

namespace Deve.Customers.Clients;

public sealed record ClientListResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string? TradeName { get; init; }
    public required string? TaxId { get; init; }
    public required string? CityName { get; init; }
    public required string? StateName { get; init; }
    public required string? CountryName { get; init; }
    public required ClientStatus Status { get; init; }
    public required decimal Balance { get; init; }
}
