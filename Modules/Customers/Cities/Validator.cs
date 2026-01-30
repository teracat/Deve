namespace Deve.Customers.Cities;

internal static class Validator
{
    public static ResultBuilder CheckCityFields(this ResultBuilder builder, string? name, Guid? stateId) =>
        builder.CheckNotNullOrEmpty(new Field(name), new Field(stateId))
               .CheckStringMaxLength(name, 50);
}
