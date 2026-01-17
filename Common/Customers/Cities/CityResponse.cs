namespace Deve.Customers.Cities;

public sealed record CityResponse(Guid Id, string Name, Guid StateId, string? StateName, string? CountryName);
