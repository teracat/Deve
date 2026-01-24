namespace Deve.Identity.Users.Delete;

internal sealed class Handler(
    IDataOptions options,
    IRepository<User> repositoryUser) : ICommandDeleteHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        if (!await repositoryUser.DeleteAsync(command.Id, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
