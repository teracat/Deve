namespace Deve.DataSource
{
    public class DataSourceFactory : IDataSourceFactory
    {
        private static DataSourceConfig? _config;

        public static void SetConfig(DataSourceConfig config) => _config = config;

        public static IDataSource Get(DataOptions? options = null) => new DataSourceMain(_config, options);

        #region IDataSourceFactory
        public void UseConfig(DataSourceConfig config) => SetConfig(config);

        public IDataSource Create(DataOptions? options = null) => Get(options);
        #endregion
    }
}