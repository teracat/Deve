namespace Deve.MODULE_NAME.FEATURE_PLURAL.METHOD_NAME;

internal sealed class Handler(
    IDataOptions options,
    IRepository<FEATURE_SINGULAR> repositoryFEATURE_SINGULAR) : IGetQueryHandler<Query, FEATURE_SINGULARMETHOD_NAMEResponse>
{
    public Task<ResultGet<FEATURE_SINGULARMETHOD_NAMEResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var query = repositoryFEATURE_SINGULAR.GetAsQueryable()
                                  .Where(x => x.Id == request.Id);

            var item = query.FirstOrDefault();
            if (item is null)
            {
                return Result.FailGet<FEATURE_SINGULARMETHOD_NAMEResponse>(options.LangCode, ResultErrorType.NotFound);
            }

            return Result.OkGet(new FEATURE_SINGULARMETHOD_NAMEResponse()
            {
                Id = item.Id,
                Name = item.Name
            });
        }, cancellationToken);
}
