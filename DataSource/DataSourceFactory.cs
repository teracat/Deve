using Deve.Data;
using Deve.DataSource.Config;

namespace Deve.DataSource
{
    public static class DataSourceFactory
    {
        private static DataSourceConfig? _config;

        public static void SetConfig(DataSourceConfig config) => _config = config;

        public static IDataSource Get(DataOptions? options) => new DataSourceMain(_config, options);

        public static IDataSource Get() => Get(null);
    }
}