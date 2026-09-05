namespace Deve.MODULE_NAME.FEATURE_PLURAL.Update;

internal sealed class Handler(
    IDataOptions options,
    IRepositoryRead<FEATURE_SINGULAR> repositoryFEATURE_SINGULARRead,
    IRepositoryWrite<FEATURE_SINGULAR> repositoryFEATURE_SINGULARWrite) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = repositoryFEATURE_SINGULARRead.GetAsQueryable().FirstOrDefault(x => x.Id == command.Id);
        if (entity is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.NotFound);
        }

        entity.Name = command.Name;

        if (!await repositoryFEATURE_SINGULARWrite.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
