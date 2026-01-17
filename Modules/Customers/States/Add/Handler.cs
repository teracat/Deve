namespace Deve.Customers.States.Add;

internal sealed class Handler(
    IDataOptions options,
    IRepository<State> repositoryState) : ICommandAddHandler<Command>
{
    public async Task<ResultGet<ResponseId>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        var entity = new State()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            CountryId = command.CountryId
        };

        var newId = await repositoryState.AddAsync(entity, cancellationToken);
        if (newId == Guid.Empty)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.OkGet(new ResponseId(newId));
    }
}
