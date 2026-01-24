namespace Deve.Customers.Countries.GetList;

internal sealed class Handler(
    IRepository<Country> repositoryCountry) : IGetListQueryHandler<Query, CountryResponse>
{
    public Task<ResultGetList<CountryResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var query = repositoryCountry.GetAsQueryable();

            // Apply filters
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

            // Get total count before pagination
            int totalCount = query.Count();

            // Apply ordering
            string orderBy = request.OrderBy ?? nameof(CountryGetListRequest.Name);
            query = orderBy.ToUpperInvariant() switch
            {
                "ID" => query.OrderBy(x => x.Id),
                "ISOCODE" => query.OrderBy(x => x.IsoCode),
                _ => query.OrderBy(x => x.Name),
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
