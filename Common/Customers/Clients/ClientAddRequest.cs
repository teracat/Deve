using Deve.Customers.Enums;

namespace Deve.Customers.Clients;

public sealed record ClientAddRequest(string Name, string? TradeName, string? TaxId, string? TaxName, Guid CityId, ClientStatus Status, decimal Balance);
