using Deve.Auth.TokenManagers;
using Deve.Identity.Enums;
using Deve.Identity.Users;
using Deve.Shield;

namespace Deve.Auth.Login;

internal sealed class Handler(
    IDataOptions options,
    ITokenManager tokenManager,
    IShield shield,
    IUserData dataUser,
    IValidator<LoginRequest> validator,
    IUserIdentityService identityService) : IRequestHandler<LoginRequest, ResultGet<LoginResponse>>
{
    private static readonly ShieldItemConfig shieldItemConfig = ShieldItemConfig.Create(3);

    public async Task<ResultGet<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var resShield = await shield.Protect(options, shieldItemConfig);
        if (!resShield.Success)
        {
            return Result.FailGet<LoginResponse>(resShield);
        }

        var resValidateAndFind = await ValidateAndFindUser(request, cancellationToken);

        bool succeeded = resValidateAndFind.Success && resValidateAndFind.Data is not null;

        await shield.SetAttemptResult(succeeded, options, shieldItemConfig);

        if (!resValidateAndFind.Success)
        {
            return Result.FailGet<LoginResponse>(resValidateAndFind);
        }

        if (resValidateAndFind.Data is null)
        {
            return Result.FailGet<LoginResponse>(options.LangCode, ResultErrorType.Unauthorized);
        }

        var user = resValidateAndFind.Data;
        var userIdentity = new UserIdentity(user.Id, user.Username, user.Role);
        var userToken = tokenManager.CreateToken(userIdentity);
        var loginResponse = new LoginResponse(user.Name, user.Username, user.Joined, userToken);

        identityService.UserIdentity = userIdentity;    // Set the current user identity for embedded usage

        return Result.OkGet(loginResponse);
    }

    private async Task<ResultGet<UserResponse>> ValidateAndFindUser(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var resultValidate = await validator.ValidateAsync(request, cancellationToken);
        if (!resultValidate.Success)
        {
            return Result.FailGet<UserResponse>(resultValidate);
        }

        var getUserRequest = new UserGetByUsernamePasswordRequest(request.Username, request.Password, UserActiveType.OnlyActive);

        var userResult = await dataUser.GetByUsernamePasswordAsync(getUserRequest, cancellationToken);
        if (!userResult.Success || userResult.Data is null)
        {
            return Result.FailGet<UserResponse>(options.LangCode, ResultErrorType.Unauthorized);
        }

        return Result.OkGet(userResult.Data);
    }
}
