using Deve.Hash;
using Deve.Shield;

namespace Deve.Identity.Users.GetByUsernamePassword;

internal sealed class Handler(
    IDataOptions options,
    IHash hash,
    IShield shield,
    IRepository<User> repositoryUser) : IGetQueryHandler<Query, UserResponse>
{
    private static readonly ShieldItemConfig shieldItemConfig = new(3);

    public async Task<ResultGet<UserResponse>> HandleAsync(Query request, CancellationToken cancellationToken)
    {
        var resShield = await shield.Protect(options, shieldItemConfig);
        if (!resShield.Success)
        {
            return Result.FailGet<UserResponse>(resShield);
        }

        var passwordHash = hash.Calc(request.Password);
        var query = repositoryUser.GetAsQueryable()
                                  .Where(x => x.Username == request.Username &&
                                              x.PasswordHash == passwordHash);

        switch (request.ActiveType)
        {
            case UserActiveType.OnlyActive:
                query = query.Where(x => x.Status == UserStatus.Active);
                break;
            case UserActiveType.OnlyInactive:
                query = query.Where(x => x.Status == UserStatus.Inactive);
                break;
            case UserActiveType.All:
                // No filter
                break;
            default:
                break;
        }

        var user = query.FirstOrDefault();

        await shield.SetAttemptResult(user is not null, options, shieldItemConfig);

        if (user is null)
        {
            return Result.FailGet<UserResponse>(options.LangCode, ResultErrorType.NotFound);
        }

        return Result.OkGet(user.ToResponse());
    }
}
