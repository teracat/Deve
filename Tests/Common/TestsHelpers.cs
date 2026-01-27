using Deve.Auth.TokenManagers;
using Deve.Auth.TokenManagers.Jwt;
using Deve.Crypt;
using Deve.Data;
using Deve.Hash;
using Deve.Auth;
using Deve.Identity.Enums;

namespace Deve.Tests;

public static class TestsHelpers
{
    public static IDataOptions CreateDataOptions() => new DataOptions();

    public static IHash CreateHash() => new HashSha512();

    public static ICrypt CreateCrypt() => new CryptAes(TestsConstants.CryptAesKey, TestsConstants.CryptAesIV);

    public static ITokenManager CreateTokenManager() => new JwtTokenManager(TestsConstants.JwtSigningSecretKey, TestsConstants.JwtEncryptionSecretKey);

    /// <summary>
    /// Set the TokenManagerJwt (which is used in the Api) as the default ITokenManager implementation and creates a valid token using it.
    /// </summary>
    /// <returns>Valid user token.</returns>
    public static UserToken CreateTokenValid(ITokenManager tokenManager, Role role)
    {
        ArgumentNullException.ThrowIfNull(tokenManager);

        var userIdentity = new UserIdentity(TestsConstants.ValidUserId, TestsConstants.UserUsernameValid, role);
        return tokenManager.CreateToken(userIdentity);
    }

    /// <summary>
    /// Set the TokenManagerJwt (which is used in the Api) as the default ITokenManager implementation and creates a token from an inactive user.
    /// </summary>
    /// <returns>Inactive user token.</returns>
    public static UserToken CreateTokenInactiveUser(ITokenManager tokenManager, Role role)
    {
        ArgumentNullException.ThrowIfNull(tokenManager);

        var userIdentity = new UserIdentity(TestsConstants.InactiveUserId, TestsConstants.UserUsernameInactive, role);
        return tokenManager.CreateToken(userIdentity);
    }
}
