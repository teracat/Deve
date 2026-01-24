namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL;

public sealed record ITEM_NAME_SINGULARMETHOD_NAMERequest(string Name)
{
    public static ITEM_NAME_SINGULARMETHOD_NAMERequest Create(string name) =>
        new(Name: name);
}
