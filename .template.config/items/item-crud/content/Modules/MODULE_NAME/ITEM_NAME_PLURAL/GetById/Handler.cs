namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.GetById;

internal sealed class Handler(
    IDataOptions options,
    IRepository<ITEM_NAME_SINGULAR> repositoryITEM_NAME_SINGULAR) : IGetQueryHandler<Query, ITEM_NAME_SINGULARResponse>
{
    public Task<ResultGet<ITEM_NAME_SINGULARResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var response = repositoryITEM_NAME_SINGULAR.GetAsQueryable()
                                     .Where(x => x.Id == request.Id)
                                     .Select(x => x.ToResponse())
                                     .FirstOrDefault();
            if (response is null)
            {
                return Result.FailGet<ITEM_NAME_SINGULARResponse>(options.LangCode, ResultErrorType.NotFound);
            }

            return Result.OkGet(response);
        }, cancellationToken);
}
