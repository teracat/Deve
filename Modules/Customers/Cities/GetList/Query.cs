namespace Deve.Customers.Cities.GetList;

internal sealed record Query : IRequest<ResultGetList<CityResponse>>
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public Guid? StateId { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
