namespace Deve.Customers.Entities;

internal sealed class Country
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string IsoCode { get; set; } = string.Empty;
}
