namespace Deve.Customers.Cities.GetList;

internal sealed class Handler(
    IRepository<City> repositoryCity,
    IRepository<State> repositoryState,
    IRepository<Country> repositoryCountry) : IGetListQueryHandler<Query, CityResponse>
{
    public Task<ResultGetList<CityResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var query = FullData.CreateQuery(repositoryCity, repositoryState, repositoryCountry);

            query = ApplyFilters(query, request);

            // Get total count before pagination
            int totalCount = query.Count();

            query = ApplyOrder(query, request, out string orderBy);

            // Apply pagination
            var offset = request.Offset ?? 0;
            var limit = request.Limit ?? Constants.DefaultLimit;
            var list = query.Select(x => x.ToResponse())
                            .Skip(offset)
                            .Take(limit)
                            .ToList();

            return Result.OkGetList(list, offset, limit, orderBy, totalCount);
        }, cancellationToken);

    private static IQueryable<FullData> ApplyFilters(IQueryable<FullData> query, Query request)
    {
        if (request.Id.HasValue)
        {
            query = query.Where(x => x.City.Id == request.Id.Value);
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.City.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
        }

        if (request.StateId.HasValue)
        {
            query = query.Where(x => x.City.StateId.Equals(request.StateId));
        }

        return query;
    }

    private static IQueryable<FullData> ApplyOrder(IQueryable<FullData> query, Query request, out string orderBy)
    {
        orderBy = request.OrderBy ?? nameof(CityGetListRequest.Name);

        return orderBy.ToUpperInvariant() switch
        {
            "ID" => query.OrderBy(x => x.City.Id),
            "STATEID" => query.OrderBy(x => x.City.StateId),
            _ => query.OrderBy(x => x.City.Name),
        };
    }
}
