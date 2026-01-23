namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL;

public sealed record ITEM_NAME_SINGULARMETHOD_NAMERequest(Guid? Id, string? Name, int? Offset, int? Limit, string? OrderBy)
{
    public static ITEM_NAME_SINGULARMETHOD_NAMERequest Create(Guid? id = null, string? name = null, int? offset = null, int? limit = null, string? orderBy = null) =>
        new(
            Id: id,
            Name: name,
            Offset: offset,
            Limit: limit,
            OrderBy: orderBy);
}
