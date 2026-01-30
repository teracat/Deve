namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

internal sealed class Handler(
    IRepository<ITEM_NAME_SINGULAR> repositoryITEM_NAME_SINGULAR) : IGetListQueryHandler<Query, ITEM_NAME_SINGULARMETHOD_NAMEResponse>
{
    public Task<ResultGetList<ITEM_NAME_SINGULARMETHOD_NAMEResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var query = repositoryITEM_NAME_SINGULAR.GetAsQueryable();

            query = ApplyFilters(query, request);

            // Get total count before pagination
            int totalCount = query.Count();

            query = ApplyOrder(query, request, out string orderBy);

            // Apply pagination
            var offset = request.Offset ?? 0;
            var limit = request.Limit ?? Constants.DefaultLimit;
            var list = query.Select(x => new ITEM_NAME_SINGULARMETHOD_NAMEResponse
            {
                Id = x.Id,
                Name = x.Name
            })
            .Skip(offset)
            .Take(limit)
            .ToList();

            return Result.OkGetList(list, offset, limit, orderBy, totalCount);
        }, cancellationToken);

    private static IQueryable<ITEM_NAME_SINGULAR> ApplyFilters(IQueryable<ITEM_NAME_SINGULAR> query, Query request)
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

    private static IQueryable<ITEM_NAME_SINGULAR> ApplyOrder(IQueryable<ITEM_NAME_SINGULAR> query, Query request, out string orderBy)
    {
        orderBy = request.OrderBy ?? nameof(ITEM_NAME_SINGULARGetListRequest.Name);
        return orderBy.ToUpperInvariant() switch
        {
            "ID" => query.OrderBy(x => x.Id),
            _ => query.OrderBy(x => x.Name),
        };
    }
}
