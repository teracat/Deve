using Deve.Authenticate;
using Deve.Internal.Dto;

namespace Deve.Auth.TokenManagers
{
    /// <summary>
    /// Used to generate and validate tokens.
    /// </summary>
    public interface ITokenManager : IDisposable
    {
        /// <summary>
        /// Generates a new token.
        /// </summary>
        /// <param name="user">Generates a new token for the specified user.</param>
        /// <param name="scheme">Scheme used to generate the token.</param>
        /// <returns>The new token.</returns>
        UserToken CreateToken(User user, string scheme);

        /// <summary>
        /// Generates a new token
        /// </summary>
        /// <param name="user">Generates a new token for the specified user.</param>
        /// <returns>The new token.</returns>
        UserToken CreateToken(User user);

        /// <summary>
        /// Attempts to validate the token and retrieve the associated UserIdentity.
        /// </summary>
        /// <param name="token">The token to validate.</param>
        /// <param name="userIdentity">The associated UserIdentity.</param>
        /// <returns>If the validation is successful.</returns>
        bool TryValidateToken(string token, out UserIdentity? userIdentity);
    }
}