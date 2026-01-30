using Deve.Hash;

namespace Deve.Identity.Users.UpdatePassword;

internal sealed class Handler(
    IDataOptions options,
    IHash hash,
    IRepository<User> repositoryUser) : ICommandUpdateHandler<Command>
{
    public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
    {
        var entity = repositoryUser.GetAsQueryable()
                                   .FirstOrDefault(x => x.Id == command.Id);
        if (entity is null)
        {
            return Result.Fail(options.LangCode, ResultErrorType.NotFound);
        }

        entity.PasswordHash = hash.Calc(command.Password);

        if (!await repositoryUser.UpdateAsync(entity, cancellationToken))
        {
            return Result.Fail(options.LangCode, ResultErrorType.Unknown);
        }

        return Result.Ok();
    }
}
