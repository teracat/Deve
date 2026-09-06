using Deve.Hash;

namespace Deve.Identity.Users.UpdatePassword;

internal sealed class Handler(
    IDataOptions options,
    IHash hash,
    IRepositoryWriteUser repositoryUser) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var passwordHash = hash.Calc(command.Password);

        if (!await repositoryUser.UpdatePasswordAsync(command.Id, passwordHash, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
