namespace Deve.Customers.Stats;

public sealed record ClientStatsResponse
{
    public decimal MinBalance { get; init; } = 0;
    public decimal AvgBalance { get; init; } = 0;
    public decimal MaxBalance { get; init; } = 0;
}
