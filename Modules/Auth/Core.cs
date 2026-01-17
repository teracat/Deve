using Deve.Auth.Login;
using Deve.Auth.RefreshToken;

namespace Deve.Auth;

internal sealed class Core(
    IRequestHandler<LoginRequest, ResultGet<LoginResponse>> loginHandler,
    IRequestHandler<RefreshTokenRequest, ResultGet<RefreshTokenResponse>> refreshTokenHandler) : IAuthData
{
    public async Task<ResultGet<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken = default) => await loginHandler.HandleAsync(request, cancellationToken);

    public async Task<ResultGet<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken = default) => await refreshTokenHandler.HandleAsync(request, cancellationToken);
}
