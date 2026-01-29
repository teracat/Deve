namespace Deve.Customers.Cities;

public sealed record CityGetListRequest
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public Guid? StateId { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
