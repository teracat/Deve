namespace Deve.DataSource
{
    /// <summary>
    /// IDataSourceBuilder implementation that uses the DataSourceFactory.
    /// </summary>
    public class DataSourceBuilderFactory : IDataSourceBuilder
    {
        public IDataSourceBuilder SetConfig(DataSourceConfig config)
        {
            DataSourceFactory.SetConfig(config);
            return this;
        }

        public IDataSource Create(DataOptions? options = null) => DataSourceFactory.Get(options);
    }
}