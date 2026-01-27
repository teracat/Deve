using Deve.Customers.Enums;

namespace Deve.Customers.Clients.Add;

internal sealed record Command : IRequest<ResultGet<ResponseId>>
{
    public required string Name { get; init; }
    public required Guid CityId { get; init; }
    public required ClientStatus Status { get; init; }
    public decimal Balance { get; init; }

    public string? TradeName { get; init; }
    public string? TaxId { get; init; }
    public string? TaxName { get; init; }
}
