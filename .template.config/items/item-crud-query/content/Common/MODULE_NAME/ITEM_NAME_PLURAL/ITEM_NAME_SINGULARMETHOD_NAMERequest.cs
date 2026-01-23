namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL;

public sealed record ITEM_NAME_SINGULARMETHOD_NAMERequest(Guid? Id)
{
    public static ITEM_NAME_SINGULARMETHOD_NAMERequest Create(Guid? id = null) =>
        new(Id: id);
}
