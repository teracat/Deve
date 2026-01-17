using Deve.Customers.Enums;

namespace Deve.Customers.Clients;

public sealed record ClientResponse(
    Guid Id,
    string Name,
    string? TradeName,
    string? TaxId,
    string? TaxName,
    Guid? CityId,
    string? CityName,
    Guid? StateId,
    string? StateName,
    Guid? CountryId,
    string? CountryName,
    ClientStatus Status,
    decimal Balance);
