using Deve.DataSource;

namespace Deve.Auth
{
    public static class AuthFactory
    {
        public static IAuth Get(IDataSource? dataSource = null, DataOptions? options = null) => new AuthMain(dataSource, options);
    }
}
