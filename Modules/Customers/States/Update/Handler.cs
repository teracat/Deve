namespace Deve.Customers.States.Update;

internal sealed class Handler(
    IDataOptions options,
    IRepository<State> repositoryState) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = new State()
        {
            Id = command.Id,
            Name = command.Name,
            CountryId = command.CountryId
        };

        if (!await repositoryState.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
