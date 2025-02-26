using Deve.Auth;
using Deve.Auth.Crypt;
using Deve.Auth.Hash;
using Deve.Auth.TokenManagers;
using Deve.Auth.TokenManagers.Jwt;
using Deve.Authenticate;
using Deve.Data;
using Deve.DataSource;
using Deve.Tests.Mocks;

namespace Deve.Tests
{
    public static class TestsHelpers
    {
        public static IDataOptions CreateDataOptions() => new DataOptions();

        public static IHash CreateHash() => new HashSha512();

        public static ICrypt CreateCrypt() => new CryptAes(TestsConstants.CryptAesKey, TestsConstants.CryptAesIV);

        public static ITokenManager CreateTokenManager() => new TokenManagerJwt(TestsConstants.JwtSigningSecretKey, TestsConstants.JwtEncryptionSecretKey);

        public static IDataSource CreateDataSourceMock() => new DataSourceMock().Object;

        public static IAuth CreateAuth(ITokenManager tokenManager, IDataSource dataSource, IHash hash, IDataOptions dataOptions) => new AuthMain(tokenManager, dataSource, hash, dataOptions);

        public static IAuth CreateAuth(ITokenManager tokenManager, IHash hash, IDataOptions dataOptions) => new AuthMain(tokenManager, CreateDataSourceMock(), hash, dataOptions);

        /// <summary>
        /// Set the TokenManagerJwt (which is used in the Api) as the default ITokenManager implementation and creates a valid token using it.
        /// </summary>
        /// <returns>Valid user token.</returns>
        public static UserToken CreateTokenValid(ITokenManager tokenManager)
        {
            var user = DataMock.Users.First(x => x.Username == TestsConstants.UserUsernameValid);
            return tokenManager.CreateToken(user);
        }

        /// <summary>
        /// Set the TokenManagerJwt (which is used in the Api) as the default ITokenManager implementation and creates a token from an inactive user.
        /// </summary>
        /// <returns>Inactive user token.</returns>
        public static UserToken CreateTokenInactiveUser(ITokenManager tokenManager)
        {
            var user = DataMock.Users.First(x => x.Username == TestsConstants.UserUsernameInactive);
            return tokenManager.CreateToken(user);
        }
    }
}