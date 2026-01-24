namespace Deve.Customers.Stats.GetClientStats;

internal sealed class Handler(
    IDataOptions options,
    ICache cache,
    IRepository<Client> repositoryClient) : IGetQueryHandler<Query, ClientStatsResponse>
{
    public Task<ResultGet<ClientStatsResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            // Example of using cache: if the item is already in the cache, return it directly.
            if (cache.TryGet(nameof(ClientStatsResponse), out ClientStatsResponse stats))
            {
                return Result.OkGet(stats);
            }

            var statsAcum = repositoryClient.GetAsQueryable()
                                            .Aggregate(new GetClientStatsAcum(),
                                                (acum, client) => new GetClientStatsAcum()
                                                {
                                                    Count = acum.Count + 1,
                                                    Sum = acum.Sum + client.Balance,
                                                    Max = acum.Max.HasValue ? Math.Max(acum.Max.Value, client.Balance) : client.Balance,
                                                    Min = acum.Min.HasValue ? Math.Min(acum.Min.Value, client.Balance) : client.Balance,
                                                });
            if (statsAcum is null)
            {
                return Result.FailGet<ClientStatsResponse>(options.LangCode, ResultErrorType.NotFound);
            }

            var response = statsAcum.ToResponse();

            // If the item is not in the cache, add it.
            cache.Set(nameof(ClientStatsResponse), response);

            return Result.OkGet(response);
        }, cancellationToken);
}

internal sealed class GetClientStatsAcum
{
    public int Count { get; set; }

    public decimal Sum { get; set; }

    public decimal? Min { get; set; }

    public decimal? Max { get; set; }

    public decimal Avg => Count == 0 ? 0 : Math.Round(Sum / Count, 2);

    public ClientStatsResponse ToResponse() => new(Min ?? 0, Avg, Max ?? 0);
}
