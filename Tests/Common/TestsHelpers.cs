using Deve.Auth;
using Deve.DataSource;

namespace Deve.Tests
{
    public static class TestsHelpers
    {
        public const string ValidUsername = "teracat";
        public const string ValidPassword = "teracat";

        public static IDataSource CreateDataSource() => new DataSourceMock().Object;

        public static IAuth CreateAuth(IDataSource? dataSource = null) => AuthFactory.Get(dataSource ?? CreateDataSource());
    }
}
