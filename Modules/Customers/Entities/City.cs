namespace Deve.Customers.Entities;

internal sealed class City
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid StateId { get; set; } = Guid.Empty;
}
