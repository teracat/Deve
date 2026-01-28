using Deve.Customers.Enums;

namespace Deve.Customers.Clients;

public sealed record ClientGetListRequest
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public string? TradeName { get; init; }
    public string? TaxId { get; init; }
    public string? TaxName { get; init; }
    public Guid? CityId { get; init; }
    public ClientStatusFilterType? StatusFilterType { get; init; }

    public string? Search { get; init; }
    public int? Offset { get; init; }
    public int? Limit { get; init; }
    public string? OrderBy { get; init; }
}
