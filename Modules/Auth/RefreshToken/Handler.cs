using Deve.Auth.TokenManagers;
using Deve.Identity.Enums;
using Deve.Identity.Users;
using Deve.Shield;

namespace Deve.Auth.RefreshToken;

internal sealed class Handler(
    IDataOptions options,
    ITokenManager tokenManager,
    IShield shield,
    IUserData dataUser,
    IValidator<RefreshTokenRequest> validator,
    IUserIdentityService identityService) : IRequestHandler<RefreshTokenRequest, ResultGet<RefreshTokenResponse>>
{
    private static readonly ShieldItemConfig shieldItemConfig = ShieldItemConfig.Create(3);

    public async Task<ResultGet<RefreshTokenResponse>> HandleAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var resShield = await shield.Protect(options, shieldItemConfig);
        if (!resShield.Success)
        {
            return Result.FailGet<RefreshTokenResponse>(resShield);
        }

        var resValidateAndFind = await ValidateAndFindUserIdentity(request, cancellationToken);

        bool succeeded = resValidateAndFind.Success && resValidateAndFind.Data is not null;

        await shield.SetAttemptResult(succeeded, options, shieldItemConfig);

        if (resValidateAndFind.Data is null)
        {
            return Result.FailGet<RefreshTokenResponse>(options.LangCode, ResultErrorType.Unauthorized);
        }

        var newUserToken = tokenManager.CreateToken(resValidateAndFind.Data);
        var refreshTokenResponse = new RefreshTokenResponse(newUserToken);
        return Result.OkGet(refreshTokenResponse);
    }

    private async Task<ResultGet<UserIdentity>> ValidateAndFindUserIdentity(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var resultValidate = await validator.ValidateAsync(request, cancellationToken);
        if (!resultValidate.Success)
        {
            await shield.SetAttemptResult(false, options);
            return Result.FailGet<UserIdentity>(resultValidate);
        }

        if (!tokenManager.TryValidateToken(request.Token, out var userIdentity) || userIdentity is null)
        {
            await shield.SetAttemptResult(false, options);
            return Result.FailGet<UserIdentity>(options.LangCode, ResultErrorType.Unauthorized);
        }

        // We need to set the identity so the call to GetByIdAsync can be authorized
        identityService.UserIdentity = userIdentity;

        // Ensure the user is still active
        var resUser = await dataUser.GetByIdAsync(userIdentity.Id, cancellationToken);
        if (!resUser.Success || resUser.Data is null || resUser.Data.Status != UserStatus.Active)
        {
            await shield.SetAttemptResult(false, options);
            return Result.FailGet<UserIdentity>(options.LangCode, ResultErrorType.Unauthorized);
        }

        return Result.OkGet(userIdentity);
    }
}
