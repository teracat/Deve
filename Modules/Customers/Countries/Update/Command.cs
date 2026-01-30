namespace Deve.Customers.Countries.Update;

internal sealed record Command : IRequest<Result>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string IsoCode { get; init; }
}
