namespace Deve.Customers.Countries.GetById;

internal sealed class Handler(
    IDataOptions options,
    IRepository<Country> repositoryCountry) : IGetQueryHandler<Query, CountryResponse>
{
    public Task<ResultGet<CountryResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var country = repositoryCountry.GetAsQueryable()
                                           .Where(x => x.Id == request.Id)
                                           .Select(x => x.ToResponse())
                                           .FirstOrDefault();
            if (country is null)
            {
                return Result.FailGet<CountryResponse>(options.LangCode, ResultErrorType.NotFound);
            }

            return Result.OkGet(country);
        }, cancellationToken);
}
