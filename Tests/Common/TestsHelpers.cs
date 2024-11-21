using Deve.Auth;
using Deve.DataSource;

namespace Deve.Tests
{
    public static class TestsHelpers
    {
        public static IDataSource CreateDataSource() => new DataSourceMock().Object;

        public static IAuth CreateAuth(IDataSource? dataSource = null) => AuthFactory.Get(dataSource ?? CreateDataSource());
    }
}
