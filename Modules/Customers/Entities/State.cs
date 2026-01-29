namespace Deve.Customers.Entities;

internal sealed class State
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid CountryId { get; set; } = Guid.Empty;
}
