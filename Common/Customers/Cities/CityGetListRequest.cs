namespace Deve.Customers.Cities;

public sealed record CityGetListRequest(Guid? Id, string? Name, Guid? StateId, int? Offset, int? Limit, string? OrderBy)
{
    public static CityGetListRequest Create(Guid? id = null, string? name = null, Guid? stateId = null, int? offset = null, int? limit = null, string? orderBy = null) =>
        new(
            Id: id,
            Name: name,
            StateId: stateId,
            Offset: offset,
            Limit: limit,
            OrderBy: orderBy);
}
