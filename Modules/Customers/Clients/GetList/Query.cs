using Deve.Customers.Enums;

namespace Deve.Customers.Clients.GetList;

internal sealed record Query(Guid? Id, string? Name, string? TradeName, string? TaxId, string? TaxName, Guid? CityId, ClientStatusFilterType? StatusFilterType, int? Offset, int? Limit, string? OrderBy) : IRequest<ResultGetList<ClientListResponse>>;
