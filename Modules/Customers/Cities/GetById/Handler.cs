namespace Deve.Customers.Cities.GetById;

internal sealed class Handler(
    IDataOptions options,
    IRepository<City> repositoryCity,
    IRepository<State> repositoryState,
    IRepository<Country> repositoryCountry) : IGetQueryHandler<Query, CityResponse>
{
    public Task<ResultGet<CityResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var city = FullData.CreateQuery(repositoryCity, repositoryState, repositoryCountry)
                                        .Where(x => x.City.Id == request.Id)
                                        .Select(x => x.ToResponse())
                                        .FirstOrDefault();
            if (city is null)
            {
                return Result.FailGet<CityResponse>(options.LangCode, ResultErrorType.NotFound);
            }

            return Result.OkGet(city);
        }, cancellationToken);
}
