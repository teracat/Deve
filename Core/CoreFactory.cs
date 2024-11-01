using Deve.DataSource;

namespace Deve.Core
{
    public static class CoreFactory
    {
        private static ICore? _core;

        public static ICore Get(bool sharedInstance = true, IDataSource? dataSource = null,  DataOptions? options = null)
        {
            if (sharedInstance)
                return _core ??= new CoreMain(true, dataSource, options);
            else
                return new CoreMain(false, dataSource, options);
        }
    }
}