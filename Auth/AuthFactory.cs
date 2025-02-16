using Deve.Data;
using Deve.DataSource;
using Deve.Auth.TokenManagers;

namespace Deve.Auth
{
    public static class AuthFactory
    {
        public static IAuth Get(ITokenManager tokenManager, IDataSource dataSource, DataOptions? options) => new AuthMain(tokenManager, dataSource, options);
        
        public static IAuth Get(ITokenManager tokenManager, IDataSource dataSource) => new AuthMain(tokenManager, dataSource, null);
    }
}