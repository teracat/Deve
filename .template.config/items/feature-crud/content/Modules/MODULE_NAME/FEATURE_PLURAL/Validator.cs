namespace Deve.MODULE_NAME.FEATURE_PLURAL;

internal static class Validator
{
    public static ResultBuilder CheckFEATURE_SINGULARFields(this ResultBuilder builder, string name) =>
        builder.CheckNotNullOrEmpty(new Field(name))
               .CheckStringMaxLength(name, 50);
}
