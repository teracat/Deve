namespace Deve.Customers.Stats;

public sealed record ClientStatsResponse
{
    public required decimal MinBalance { get; init; }
    public required decimal AvgBalance { get; init; }
    public required decimal MaxBalance { get; init; }
}
