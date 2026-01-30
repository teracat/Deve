namespace Deve.Customers.Cities.Add;

internal sealed record Command : IRequest<ResultGet<ResponseId>>
{
    public required string Name { get; init; }
    public required Guid StateId { get; init; }
}
