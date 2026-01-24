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

            // Apply filters
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

            // Get total count before pagination
            int totalCount = query.Count();

            // Apply ordering
            string orderBy = request.OrderBy ?? nameof(CityGetListRequest.Name);
            query = orderBy.ToUpperInvariant() switch
            {
                "ID" => query.OrderBy(x => x.City.Id),
                "STATEID" => query.OrderBy(x => x.City.StateId),
                _ => query.OrderBy(x => x.City.Name),
            };

            // Apply pagination
            var offset = request?.Offset ?? 0;
            var limit = request?.Limit ?? Constants.DefaultCriteriaLimit;
            var list = query.Select(x => x.ToResponse())
                            .Skip(offset)
                            .Take(limit)
                            .ToList();

            return Result.OkGetList(list, offset, limit, orderBy, totalCount);
        }, cancellationToken);
}
