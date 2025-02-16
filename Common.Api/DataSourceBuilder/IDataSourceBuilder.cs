using Deve.Data;
using Deve.DataSource;
using Deve.DataSource.Config;

namespace Deve.Api.DataSourceBuilder
{
    public interface IDataSourceBuilder
    {
        IDataSourceBuilder SetConfig(DataSourceConfig config);

        IDataSource Create(DataOptions? options);

        IDataSource Create();
    }
}