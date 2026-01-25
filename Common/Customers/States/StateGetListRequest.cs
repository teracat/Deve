namespace Deve.Customers.States;

public sealed record StateGetListRequest
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public Guid? CountryId { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
