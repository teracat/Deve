namespace Deve.Identity.Users.Update;

internal sealed class Handler(
    IDataOptions options,
    IRepositoryWriteUser repositoryUserWrite) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = new User
        {
            Id = command.Id,
            Name = command.Name.Trim(),
            Username = command.Username.Trim(),
            Status = command.Status,
            Role = command.Role,
            Email = command.Email?.Trim(),
            Birthday = command.Birthday
        };

        if (!await repositoryUserWrite.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
