using Deve.Auth.TokenManagers;
using Deve.Data;
using Deve.DataSource;

namespace Deve.Core
{
    public static class CoreFactory
    {
        private static ICore? _core;

        public static ICore Get(bool sharedInstance, ITokenManager tokenManager, IDataSource? dataSource, DataOptions? options)
        {
            if (sharedInstance)
            {
                return _core ??= new CoreMain(true, tokenManager, dataSource, options);
            }
            else
            {
                return new CoreMain(false, tokenManager, dataSource, options);
            }
        }

        public static ICore Get(bool sharedInstance, ITokenManager tokenManager, IDataSource dataSource) => Get(sharedInstance, tokenManager, dataSource, null);

        public static ICore Get(bool sharedInstance, ITokenManager tokenManager, DataOptions? options) => Get(sharedInstance, tokenManager, null, options);

        public static ICore Get(bool sharedInstance, ITokenManager tokenManager) => Get(sharedInstance, tokenManager, null, null);
    }
}