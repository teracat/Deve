namespace Deve.Identity.Users.Update;

internal sealed class Handler(
    IDataOptions options,
    IRepositoryRead<User> repositoryUserRead,
    IRepositoryWrite<User> repositoryUserWrite) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = repositoryUserRead.GetAsQueryable()
                                       .FirstOrDefault(x => x.Id == command.Id);
        if (entity is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.NotFound);
        }

        entity.Name = command.Name.Trim();
        entity.Username = command.Username.Trim();
        entity.Status = command.Status;
        entity.Role = command.Role;
        entity.Email = command.Email?.Trim();
        entity.Birthday = command.Birthday;

        if (!await repositoryUserWrite.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
