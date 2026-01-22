namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.Add;

internal sealed class Handler(
    IDataOptions options,
    IRepository<ITEM_NAME_SINGULAR> repositoryITEM_NAME_SINGULAR) : ICommandAddHandler<Command>
{
    public async Task<ResultGet<ResponseId>> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        var entity = new ITEM_NAME_SINGULAR()
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };

        var newId = await repositoryITEM_NAME_SINGULAR.AddAsync(entity, cancellationToken);
        if (newId == Guid.Empty)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.OkGet(new ResponseId(newId));
    }
}
