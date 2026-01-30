namespace Deve.Customers.Countries;

internal static class Validator
{
    public static ResultBuilder CheckCountryFields(this ResultBuilder builder, string? name, string? isoCode) =>
        builder.CheckNotNullOrEmpty(new Field(name), new Field(isoCode))
               .CheckStringMaxLength(name, 50)
               .CheckStringMinLength(isoCode, 2)
               .CheckStringMaxLength(isoCode, 2);
}
