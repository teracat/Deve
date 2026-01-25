namespace Deve.Customers.States.Add;

internal sealed record Command : IRequest<ResultGet<ResponseId>>
{
    public required string Name { get; init; }
    public required Guid CountryId { get; init; }
}
