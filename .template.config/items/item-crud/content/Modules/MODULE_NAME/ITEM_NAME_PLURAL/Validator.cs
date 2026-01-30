namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL;

internal static class Validator
{
    public static ResultBuilder CheckITEM_NAME_SINGULARFields(this ResultBuilder builder, string name) =>
        builder.CheckNotNullOrEmpty(new Field(name))
               .CheckStringMaxLength(name, 50);
}
