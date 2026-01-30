namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

internal sealed class Handler(
    IDataOptions options,
    IRepository<ITEM_NAME_SINGULAR> repositoryITEM_NAME_SINGULAR) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = repositoryITEM_NAME_SINGULAR.GetAsQueryable()
                                   .FirstOrDefault(x => x.Id == command.Id);
        if (entity is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.NotFound);
        }

        entity.Name = command.Name;

        if (!await repositoryITEM_NAME_SINGULAR.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
