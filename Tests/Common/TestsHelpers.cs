using Deve.Auth;
using Deve.DataSource;

namespace Deve.Tests
{
    public static class TestsHelpers
    {
        public const string UserUsernameValid = "tests";
        public const string UserPasswordValid = "tests";

        public const string UserUsernameInactive = "tests2";
        public const string UserPasswordInactive = "tests2";

        public static IDataSource CreateDataSourceMock() => new DataSourceMock().Object;

        public static IAuth CreateAuth(IDataSource? dataSource = null) => AuthFactory.Get(dataSource ?? CreateDataSourceMock());
    }
}
