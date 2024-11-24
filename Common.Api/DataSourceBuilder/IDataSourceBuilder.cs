using Deve.DataSource;

namespace Deve.Api
{
    public interface IDataSourceBuilder
    {
        IDataSourceBuilder SetConfig(DataSourceConfig config);

        IDataSource Create(DataOptions? options = null);
    }
}