namespace Deve.Identity.Users.Update;

internal sealed class Handler(
    IDataOptions options,
    IRepository<User> repositoryUser) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = repositoryUser.GetAsQueryable().FirstOrDefault(x => x.Id == command.Id);
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

        if (!await repositoryUser.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
