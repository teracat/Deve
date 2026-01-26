namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

internal sealed class Handler(
    IDataOptions options,
    IRepository<ITEM_NAME_SINGULAR> repositoryITEM_NAME_SINGULAR) : IGetQueryHandler<Query, ITEM_NAME_SINGULARMETHOD_NAMEResponse>
{
    public Task<ResultGet<ITEM_NAME_SINGULARMETHOD_NAMEResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var query = repositoryITEM_NAME_SINGULAR.GetAsQueryable()
                                  .Where(x => x.Id == request.Id);

            var item = query.FirstOrDefault();
            if (item is null)
            {
                return Result.FailGet<ITEM_NAME_SINGULARMETHOD_NAMEResponse>(options.LangCode, ResultErrorType.NotFound);
            }

            return Result.OkGet(new ITEM_NAME_SINGULARMETHOD_NAMEResponse()
            {
                Id = item.Id,
                Name = item.Name
            });
        }, cancellationToken);
}
