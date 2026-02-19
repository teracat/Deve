using Deve.Dto.Responses.Results;
using Deve.Auth.Login;
using Deve.Auth.RefreshToken;

namespace Deve.Auth;

/// <summary>
/// Provides authentication functionality.
/// </summary>
public interface IAuthData : IFeature
{
    /// <summary>
    /// Authenticates a user and generates an access token.
    /// </summary>
    /// <param name="request">Credentials used to validate the user.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>Returns a new token if authentication is successful.</returns>
    Task<ResultGet<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken);
    Task<ResultGet<LoginResponse>> Login(LoginRequest request) => Login(request, CancellationToken.None);


    /// <summary>
    /// Refreshes the authentication token for an active session.
    /// </summary>
    /// <param name="request">The token to refresh.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A new token if the refresh operation is successful.</returns>
    Task<ResultGet<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken);
    Task<ResultGet<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest request) => RefreshToken(request, CancellationToken.None);
}
