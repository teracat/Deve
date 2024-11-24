using Deve.Auth;
using Deve.DataSource;

namespace Deve.Tests
{
    public static class TestsHelpers
    {
        public static IDataSource CreateDataSourceMock() => new DataSourceMock().Object;

        public static IAuth CreateAuth(IDataSource? dataSource = null) => AuthFactory.Get(dataSource ?? CreateDataSourceMock());

        public static UserToken CreateTokenValid(IAuth? auth = null)
        {
            auth ??= CreateAuth();

            var user = DataMock.Users.First(x => x.Username == TestsConstants.UserUsernameValid);
            return auth.TokenManager.CreateToken(user);
        }

        public static UserToken CreateTokenInactiveUser(IAuth? auth = null)
        {
            auth ??= CreateAuth();

            var user = DataMock.Users.First(x => x.Username == TestsConstants.UserUsernameInactive);
            return auth.TokenManager.CreateToken(user);
        }
    }
}
