using Deve.Auth;
using Deve.DataSource;

namespace Deve.Tests
{
    public static class TestsHelpers
    {
        public const string UserUsernameValid = "teracat";
        public const string UserPasswordValid = "teracat";

        public const string UserUsernameInactive = "dan.brown";
        public const string UserPasswordInactive = "dan.brown";

        public static IDataSource CreateDataSourceMock() => new DataSourceMock().Object;

        public static IAuth CreateAuth(IDataSource? dataSource = null) => AuthFactory.Get(dataSource ?? CreateDataSourceMock());
    }
}
