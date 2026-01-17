namespace Deve.Customers.Countries;

public sealed record CountryGetListRequest(Guid? Id, string? Name, string? IsoCode, int? Offset, int? Limit, string? OrderBy)
{
    public static CountryGetListRequest Create(Guid? id = null, string? name = null, string? isoCode = null, int? offset = null, int? limit = null, string? orderBy = null) =>
        new(
            Id: id,
            Name: name,
            IsoCode: isoCode,
            Offset: offset,
            Limit: limit,
            OrderBy: orderBy);
}
