namespace Deve.MODULE_NAME.FEATURE_PLURAL.GetById;

internal sealed class Handler(
    IDataOptions options,
    IRepository<FEATURE_SINGULAR> repositoryFEATURE_SINGULAR) : IGetQueryHandler<Query, FEATURE_SINGULARResponse>
{
    public Task<ResultGet<FEATURE_SINGULARResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var response = repositoryFEATURE_SINGULAR.GetAsQueryable()
                                     .Where(x => x.Id == request.Id)
                                     .Select(x => x.ToResponse())
                                     .FirstOrDefault();
            if (response is null)
            {
                return Result.FailGet<FEATURE_SINGULARResponse>(options.LangCode, ResultErrorType.NotFound);
            }

            return Result.OkGet(response);
        }, cancellationToken);
}
