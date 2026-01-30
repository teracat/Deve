using Deve.Hash;

namespace Deve.Identity.Users.Add;

internal sealed class Handler(
    IDataOptions options,
    IHash hash,
    IRepository<User> repositoryUser) : ICommandAddHandler<Command>
{
    public async Task<ResultGet<ResponseId>> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = new User()
        {
            Id = Guid.NewGuid(),
            Name = command.Name.Trim(),
            Username = command.Username.Trim(),
            Status = command.Status,
            Joined = DateTimeOffset.UtcNow,
            Role = command.Role,
            Email = command.Email?.Trim(),
            Birthday = command.Birthday,
            PasswordHash = hash.Calc(command.Password)
        };

        var newId = await repositoryUser.AddAsync(entity, cancellationToken);
        if (newId == Guid.Empty)
        {
            return Result.FailGet<ResponseId>(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.OkGet(new ResponseId(newId));
    }
}
