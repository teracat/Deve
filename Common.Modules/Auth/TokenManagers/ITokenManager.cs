namespace Deve.Auth.TokenManagers;

/// <summary>
/// Used to generate and validate tokens.
/// </summary>
public interface ITokenManager : IDisposable
{
    /// <summary>
    /// Generates a new token.
    /// </summary>
    /// <param name="identity">The identity associated with the token.</param>
    /// <param name="scheme">Scheme used to generate the token.</param>
    /// <returns>The new token.</returns>
    UserToken CreateToken(UserIdentity identity, string scheme);

    /// <summary>
    /// Generates a new token
    /// </summary>
    /// <param name="identity">The identity associated with the token.</param>
    /// <returns>The new token.</returns>
    UserToken CreateToken(UserIdentity identity);

    /// <summary>
    /// Attempts to validate the token and retrieve the associated UserIdentity.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <param name="identity">The associated UserIdentity.</param>
    /// <returns>If the validation is successful.</returns>
    bool TryValidateToken(string token, out UserIdentity? identity);
}
