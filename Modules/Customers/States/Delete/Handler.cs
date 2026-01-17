namespace Deve.Customers.States.Delete;

internal sealed class Handler(
    IDataOptions options,
    IRepository<State> repositoryState,
    IRepository<City> repositoryCity) : ICommandDeleteHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        var hasCities = (from city in repositoryCity.GetAsQueryable()
                         where city.StateId == command.Id
                         select city).Any();
        if (hasCities)
        {
            return Result.Fail(options.LangCode, ResultErrorType.NotAllowed);
        }

        if (!await repositoryState.DeleteAsync(command.Id, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
