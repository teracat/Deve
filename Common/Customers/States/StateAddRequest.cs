namespace Deve.Customers.States;

public sealed record StateAddRequest
{
    public string Name { get; init; } = string.Empty;
    public Guid CountryId { get; init; } = Guid.Empty;
}
