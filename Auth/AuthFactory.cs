using Deve.Auth.TokenManagers;
using Deve.Data;
using Deve.DataSource;

namespace Deve.Auth
{
    public static class AuthFactory
    {
        public static IAuth Get(IDataSource dataSource, DataOptions? options, ITokenManager tokenManager) => new AuthMain(dataSource, options, tokenManager);
        
        public static IAuth Get(IDataSource dataSource, ITokenManager tokenManager) => new AuthMain(dataSource, null, tokenManager);

        public static IAuth Get(IDataSource dataSource, DataOptions? options) => new AuthMain(dataSource, options);

        public static IAuth Get(IDataSource dataSource) => new AuthMain(dataSource);
    }
}