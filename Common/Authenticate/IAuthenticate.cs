using Deve.Dto;

namespace Deve.Authenticate
{
    /// <summary>
    /// Provides authentication functionality.
    /// </summary>
    public interface IAuthenticate
    {
        /// <summary>
        /// Authenticates a user and generates an access token.
        /// </summary>
        /// <param name="userCredentials">Credentials used to validate the user.</param>
        /// <returns>Returns a new token if authentication is successful.</returns>
        Task<ResultGet<UserToken>> Login(UserCredentials userCredentials);

        /// <summary>
        /// Refreshes the authentication token for an active session.
        /// </summary>
        /// <param name="token">The token to refresh.</param>
        /// <returns>A new token if the refresh operation is successful.</returns>
        Task<ResultGet<UserToken>> RefreshToken(string token);
    }
}
