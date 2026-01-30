namespace Deve.Customers.Clients.Update;

internal sealed record Command : IRequest<Result>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required Guid CityId { get; init; }
}
