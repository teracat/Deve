using Deve.Customers.Enums;

namespace Deve.Customers.Clients;

public sealed record ClientListResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? TradeName { get; init; }
    public string? TaxId { get; init; }
    public string? CityName { get; init; }
    public string? StateName { get; init; }
    public string? CountryName { get; init; }
    public ClientStatus Status { get; init; } = ClientStatus.Inactive;
    public decimal Balance { get; init; } = 0;
}
