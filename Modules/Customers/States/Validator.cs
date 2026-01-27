namespace Deve.Customers.States;

internal static class Validator
{
    public static ResultBuilder CheckStateFields(this ResultBuilder builder, string? name, Guid? countryId) =>
        builder.CheckNotNullOrEmpty(new Field(name), new Field(countryId))
               .CheckStringMaxLength(name, 50);
}
