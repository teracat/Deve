namespace Deve.Customers.Clients.GetById;

internal sealed class Handler(
    IDataOptions options,
    IRepository<Client> repositoryClient,
    IRepository<City> repositoryCity,
    IRepository<State> repositoryState,
    IRepository<Country> repositoryCountry,
    ICache cache) : IGetQueryHandler<Query, ClientResponse>
{
    public Task<ResultGet<ClientResponse>> HandleAsync(Query request, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            var cacheKey = Utils.GetCacheKeyForType<ClientResponse>(request.Id);
            if (cache.TryGet(cacheKey, out ClientResponse value))
            {
                return Result.OkGet(value);
            }

            var response = FullData.CreateQuery(repositoryClient, repositoryCity, repositoryState, repositoryCountry)
                                         .Where(x => x.Client.Id == request.Id)
                                         .Select(x => x.ToResponse())
                                         .FirstOrDefault();
            if (response is null)
            {
                return Result.FailGet<ClientResponse>(options.LangCode, ResultErrorType.NotFound);
            }

            cache.Set(cacheKey, response);

            return Result.OkGet(response);
        }, cancellationToken);
}
