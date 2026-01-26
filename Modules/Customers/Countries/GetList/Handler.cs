namespace Deve.Customers.Countries.GetList;

internal sealed class Handler(
    IRepository<Country> repositoryCountry) : IGetListQueryHandler<Query, CountryResponse>
{
    public Task<ResultGetList<CountryResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var query = repositoryCountry.GetAsQueryable();

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

    private static IQueryable<Country> ApplyFilters(IQueryable<Country> query, Query request)
    {
        if (request.Id.HasValue)
        {
            query = query.Where(x => x.Id == request.Id.Value);
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(request.IsoCode))
        {
            query = query.Where(x => x.IsoCode.Equals(request.IsoCode, StringComparison.OrdinalIgnoreCase));
        }

        return query;
    }

    private static IQueryable<Country> ApplyOrder(IQueryable<Country> query, Query request, out string orderBy)
    {
        orderBy = request.OrderBy ?? nameof(CountryGetListRequest.Name);
        return orderBy.ToUpperInvariant() switch
        {
            "ID" => query.OrderBy(x => x.Id),
            "ISOCODE" => query.OrderBy(x => x.IsoCode),
            _ => query.OrderBy(x => x.Name),
        };
    }
}
