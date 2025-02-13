using Deve.Data;
using Deve.DataSource;

namespace Deve.Core
{
    public static class CoreFactory
    {
        private static ICore? _core;

        public static ICore Get(bool sharedInstance, IDataSource? dataSource, DataOptions? options)
        {
            if (sharedInstance)
            {
                return _core ??= new CoreMain(true, dataSource, options);
            }
            else
            {
                return new CoreMain(false, dataSource, options);
            }
        }

        public static ICore Get(bool sharedInstance, IDataSource dataSource) => Get(sharedInstance, dataSource, null);

        public static ICore Get(bool sharedInstance) => Get(sharedInstance, null, null);
    }
}