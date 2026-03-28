using Deve.Hash;

namespace Deve.Identity.Users.UpdatePassword;

internal sealed class Handler(
    IDataOptions options,
    IHash hash,
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

        entity.PasswordHash = hash.Calc(command.Password);

        if (!await repositoryUserWrite.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
