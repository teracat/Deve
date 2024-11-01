namespace Deve.DataSource
{
    public static class DataSourceFactory
    {
        private static DataSourceConfig? _config;

        public static void SetConfig(DataSourceConfig config) => _config = config;

        public static IDataSource Get(DataOptions? options = null) => new DataSourceMain(_config, options);
    }
}