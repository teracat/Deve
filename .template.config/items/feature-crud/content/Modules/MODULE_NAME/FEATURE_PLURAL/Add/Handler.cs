namespace Deve.MODULE_NAME.FEATURE_PLURAL.Add;

internal sealed class Handler(
    IDataOptions options,
    IRepository<FEATURE_SINGULAR> repositoryFEATURE_SINGULAR) : ICommandAddHandler<Command>
{
    public async Task<ResultGet<ResponseId>> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = new FEATURE_SINGULAR()
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };

        var newId = await repositoryFEATURE_SINGULAR.AddAsync(entity, cancellationToken);
        if (newId == Guid.Empty)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.OkGet(new ResponseId(newId));
    }
}
