namespace Deve.MODULE_NAME.FEATURE_PLURAL.GetList;

internal sealed class Handler(
    IRepository<FEATURE_SINGULAR> repositoryFEATURE_SINGULAR) : IGetListQueryHandler<Query, FEATURE_SINGULARResponse>
{
    public Task<ResultGetList<FEATURE_SINGULARResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var query = repositoryFEATURE_SINGULAR.GetAsQueryable();

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

    private static IQueryable<FEATURE_SINGULAR> ApplyFilters(IQueryable<FEATURE_SINGULAR> query, Query request)
    {
        if (request.Id.HasValue)
        {
            query = query.Where(x => x.Id == request.Id.Value);
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(x => x.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
        }

        return query;
    }

    private static IQueryable<FEATURE_SINGULAR> ApplyOrder(IQueryable<FEATURE_SINGULAR> query, Query request, out string orderBy)
    {
        orderBy = request.OrderBy ?? nameof(FEATURE_SINGULARGetListRequest.Name);
        return orderBy.ToUpperInvariant() switch
        {
            "ID" => query.OrderBy(x => x.Id),
            _ => query.OrderBy(x => x.Name),
        };
    }
}
