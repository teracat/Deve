using Deve.Data;
using Deve.DataSource;
using Deve.DataSource.Config;

namespace Deve.Api.DataSourceBuilder
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

        public IDataSource Create(DataOptions? options) => DataSourceFactory.Get(options);

        public IDataSource Create() => Create(null);
    }
}