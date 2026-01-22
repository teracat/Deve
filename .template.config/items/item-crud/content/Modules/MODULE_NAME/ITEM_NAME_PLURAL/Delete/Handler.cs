namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.Delete;

internal sealed class Handler(
    IDataOptions options,
    IRepository<ITEM_NAME_SINGULAR> repositoryITEM_NAME_SINGULAR) : ICommandDeleteHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken = default)
    {
        if (!await repositoryITEM_NAME_SINGULAR.DeleteAsync(command.Id, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
