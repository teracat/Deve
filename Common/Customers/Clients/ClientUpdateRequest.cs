using Deve.Customers.Enums;

namespace Deve.Customers.Clients;

public sealed record ClientUpdateRequest()
{
    public required string Name { get; init; }
    public required Guid CityId { get; init; }
    public required ClientStatus Status { get; init; }
    public required decimal Balance { get; init; }

    public string? TradeName { get; init; }
    public string? TaxId { get; init; }
    public string? TaxName { get; init; }
}
