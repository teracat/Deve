using Deve.Customers.Enums;

namespace Deve.Customers.Clients;

public sealed record ClientAddRequest
{
    public string Name { get; init; } = string.Empty;
    public Guid CityId { get; init; } = Guid.Empty;
    public ClientStatus Status { get; init; } = ClientStatus.Inactive;
    public decimal Balance { get; init; } = 0;

    public string? TradeName { get; init; }
    public string? TaxId { get; init; }
    public string? TaxName { get; init; }
}
