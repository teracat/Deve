using Deve.Customers.Enums;

namespace Deve.Customers.Entities;

internal sealed class Client
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string? TradeName { get; set; }
    public string? TaxId { get; set; }
    public string? TaxName { get; set; }
    public Guid? CityId { get; set; } = Guid.Empty;
    public ClientStatus Status { get; set; } = ClientStatus.Inactive;
    public decimal Balance { get; set; } = 0;
}
