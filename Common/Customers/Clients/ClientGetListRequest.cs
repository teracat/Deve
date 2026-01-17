using Deve.Customers.Enums;

namespace Deve.Customers.Clients;

public sealed record ClientGetListRequest(Guid? Id, string? Name, string? TradeName, string? TaxId, string? TaxName, Guid? CityId, ClientStatusFilterType? StatusFilterType, int? Offset, int? Limit, string? OrderBy)
{
    public static ClientGetListRequest Create(Guid? id = null, string? name = null, string? tradeName = null, string? taxId = null, string? taxName = null, Guid? cityId = null, ClientStatusFilterType? statusFilterType = null, int? offset = null, int? limit = null, string? orderBy = null) =>
        new(
            Id: id,
            Name: name,
            TradeName: tradeName,
            TaxId: taxId,
            TaxName: taxName,
            CityId: cityId,
            StatusFilterType: statusFilterType,
            Offset: offset,
            Limit: limit,
            OrderBy: orderBy);
}
