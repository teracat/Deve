using Deve.Customers.Enums;

namespace Deve.Customers.Clients.Add;

internal sealed record Command(string Name, string? TradeName, string? TaxId, string? TaxName, Guid CityId, ClientStatus Status, decimal Balance) : IRequest<ResultGet<ResponseId>>;
