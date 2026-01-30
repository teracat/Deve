namespace Deve.Customers.Clients;

internal static class Validator
{
    public static ResultBuilder CheckClientFields(this ResultBuilder builder, string? name, Guid? cityId) =>
        builder.CheckNotNullOrEmpty(new Field(name), new Field(cityId))
               .CheckStringMaxLength(name, 50);
}
