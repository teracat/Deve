namespace Deve.Customers.Countries.Add;

internal sealed record Command : IRequest<ResultGet<ResponseId>>
{
    public required string Name { get; init; }
    public required string IsoCode { get; init; }
}
