namespace Deve.Customers.States;

public sealed record StateResponse(Guid Id, string Name, Guid CountryId, string? CountryName);
