namespace Deve.Customers.States.GetById;

internal sealed class Handler(
    IDataOptions options,
    IRepository<State> repositoryState,
    IRepository<Country> repositoryCountry) : IGetQueryHandler<Query, StateResponse>
{
    public Task<ResultGet<StateResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var state = FullData.CreateQuery(repositoryState, repositoryCountry)
                                     .Where(x => x.State.Id == request.Id)
                                     .Select(x => x.ToResponse())
                                     .FirstOrDefault();
            if (state is null)
            {
                return Result.FailGet<StateResponse>(options.LangCode, ResultErrorType.NotFound);
            }

            return Result.OkGet(state);
        }, cancellationToken);
}
