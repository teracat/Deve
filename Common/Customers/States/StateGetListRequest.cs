namespace Deve.Customers.States;

public sealed record StateGetListRequest(Guid? Id, string? Name, Guid? CountryId, int? Offset, int? Limit, string? OrderBy)
{
    public static StateGetListRequest Create(Guid? id = null, string? name = null, Guid? countryId = null, int? offset = null, int? limit = null, string? orderBy = null) =>
        new(
            Id: id,
            Name: name,
            CountryId: countryId,
            Offset: offset,
            Limit: limit,
            OrderBy: orderBy);
}
