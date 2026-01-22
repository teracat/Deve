namespace Deve.Customers.States.GetList;

internal sealed class Handler(
    IRepository<State> repositoryState,
    IRepository<Country> repositoryCountry) : IGetListQueryHandler<Query, StateResponse>
{
    public Task<ResultGetList<StateResponse>> HandleAsync(Query request, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            var query = FullData.CreateQuery(repositoryState, repositoryCountry);

            // Apply filters
            if (request.Id.HasValue)
            {
                query = query.Where(x => x.State.Id == request.Id.Value);
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.State.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
            }
            if (request.CountryId.HasValue)
            {
                query = query.Where(x => x.State.CountryId.Equals(request.CountryId));
            }

            // Get total count before pagination
            int totalCount = query.Count();

            // Apply ordering
            string orderBy = request.OrderBy ?? nameof(StateGetListRequest.Name);
            query = orderBy.ToUpperInvariant() switch
            {
                "ID" => query.OrderBy(x => x.State.Id),
                "COUNTRYID" => query.OrderBy(x => x.State.CountryId),
                _ => query.OrderBy(x => x.State.Name),
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
