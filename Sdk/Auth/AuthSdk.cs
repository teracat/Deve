using Deve.Dto.Responses.Results;
using Deve.Auth;
using Deve.Auth.Login;
using Deve.Auth.RefreshToken;

namespace Deve.Sdk.Auth;

internal class AuthSdk : BaseSdk, IAuthData
{
    public AuthSdk(ISdk sdk)
        : base(sdk)
    {
    }

    public async Task<ResultGet<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var res = await PostWithResult<LoginResponse>(AuthConstants.PathAuthV1 + AuthConstants.MethodLogin, RequestAuthType.None, request, cancellationToken);
            if (res?.Data is null)
            {
                return Result.FailGet<LoginResponse>(Sdk.Options.LangCode, ResultErrorType.Unknown);
            }

            Sdk.UserToken = res.Data.Token;
            return res;
        }
        catch (Exception ex)
        {
            return Result.FailGet<LoginResponse>(ResultErrorType.Unknown, null, ex.Message);
        }
    }

    public async Task<ResultGet<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var res = await PostWithResult<RefreshTokenResponse>(AuthConstants.PathAuthV1 + AuthConstants.MethodRefreshToken, RequestAuthType.None, request, cancellationToken);
            return res ?? Result.FailGet<RefreshTokenResponse>(Sdk.Options.LangCode, ResultErrorType.Unknown);
        }
        catch (Exception ex)
        {
            return Result.FailGet<RefreshTokenResponse>(ResultErrorType.Unknown, null, ex.Message);
        }
    }
}
