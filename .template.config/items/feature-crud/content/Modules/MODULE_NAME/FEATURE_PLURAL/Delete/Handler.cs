namespace Deve.MODULE_NAME.FEATURE_PLURAL.Delete;

internal sealed class Handler(
    IDataOptions options,
    IRepository<FEATURE_SINGULAR> repositoryFEATURE_SINGULAR) : ICommandDeleteHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        if (!await repositoryFEATURE_SINGULAR.DeleteAsync(command.Id, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
