namespace Deve.Customers.Countries.Delete;

internal sealed class Handler(
    IDataOptions options,
    IRepository<Country> repositoryCountry,
    IRepository<State> repositoryState) : ICommandDeleteHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        var hasStates = (from state in repositoryState.GetAsQueryable()
                         where state.CountryId == command.Id
                         select state).Any();
        if (hasStates)
        {
            return Result.Fail(options.LangCode, ResultErrorType.NotAllowed);
        }

        if (!await repositoryCountry.DeleteAsync(command.Id, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
