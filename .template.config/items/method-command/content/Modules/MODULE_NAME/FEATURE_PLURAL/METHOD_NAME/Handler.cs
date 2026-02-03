namespace Deve.MODULE_NAME.FEATURE_PLURAL.METHOD_NAME;

internal sealed class Handler(
    IDataOptions options,
    IRepository<FEATURE_SINGULAR> repositoryFEATURE_SINGULAR) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = repositoryFEATURE_SINGULAR.GetAsQueryable()
                                   .FirstOrDefault(x => x.Id == command.Id);
        if (entity is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.NotFound);
        }

        entity.Name = command.Name;

        if (!await repositoryFEATURE_SINGULAR.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
