using Deve.Customers.Enums;

namespace Deve.Customers.Clients;

public sealed record ClientResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? TradeName { get; init; }
    public string? TaxId { get; init; }
    public string? TaxName { get; init; }
    public Guid? CityId { get; init; }
    public string? CityName { get; init; }
    public Guid? StateId { get; init; }
    public string? StateName { get; init; }
    public Guid? CountryId { get; init; }
    public string? CountryName { get; init; }
    public ClientStatus Status { get; init; } = ClientStatus.Inactive;
    public decimal Balance { get; init; } = 0;
}
