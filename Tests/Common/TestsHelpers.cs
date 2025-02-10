using Deve.Auth;
using Deve.Auth.TokenManagers;
using Deve.Auth.TokenManagers.Jwt;
using Deve.Authenticate;
using Deve.DataSource;
using Deve.Tests.Mocks;

namespace Deve.Tests
{
    public static class TestsHelpers
    {
        public static IDataSource CreateDataSourceMock() => new DataSourceMock().Object;

        public static IAuth CreateAuth(IDataSource? dataSource = null) => AuthFactory.Get(dataSource ?? CreateDataSourceMock());

        /// <summary>
        /// Set the TokenManagerJwt (which is used in the Api) as the default ITokenManager implementation and creates a valid token using it.
        /// </summary>
        /// <returns>Valid user token.</returns>
        public static UserToken CreateTokenValid()
        {
            var tokenManager = new TokenManagerJwt();
            TokenManagerFactory.Set(ApiConstants.AuthDefaultScheme, tokenManager);
            var user = DataMock.Users.First(x => x.Username == TestsConstants.UserUsernameValid);
            return tokenManager.CreateToken(user);
        }

        /// <summary>
        /// Set the TokenManagerJwt (which is used in the Api) as the default ITokenManager implementation and creates a token from an inactive user.
        /// </summary>
        /// <returns>Inactive user token.</returns>
        public static UserToken CreateTokenInactiveUser()
        {
            var tokenManager = new TokenManagerJwt();
            TokenManagerFactory.Set(ApiConstants.AuthDefaultScheme, tokenManager);
            var user = DataMock.Users.First(x => x.Username == TestsConstants.UserUsernameInactive);
            return tokenManager.CreateToken(user);
        }
    }
}