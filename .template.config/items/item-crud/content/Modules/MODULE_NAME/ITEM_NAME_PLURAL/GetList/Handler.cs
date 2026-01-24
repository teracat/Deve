namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.GetList;

internal sealed class Handler(
    IRepository<ITEM_NAME_SINGULAR> repositoryITEM_NAME_SINGULAR) : IGetListQueryHandler<Query, ITEM_NAME_SINGULARResponse>
{
    public Task<ResultGetList<ITEM_NAME_SINGULARResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var query = repositoryITEM_NAME_SINGULAR.GetAsQueryable();

            // Apply filters
            if (request.Id.HasValue)
            {
                query = query.Where(x => x.Id == request.Id.Value);
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
            }

            // Get total count before pagination
            int totalCount = query.Count();

            // Apply ordering
            string orderBy = request.OrderBy ?? nameof(ITEM_NAME_SINGULARGetListRequest.Name);
            query = orderBy.ToUpperInvariant() switch
            {
                "ID" => query.OrderBy(x => x.Id),
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
