using Deve.Customers.Enums;

namespace Deve.Customers.Clients;

public sealed record ClientListResponse(
    Guid Id,
    string Name,
    string? TradeName,
    string? TaxId,
    string? CityName,
    string? StateName,
    string? CountryName,
    ClientStatus Status,
    decimal Balance);
