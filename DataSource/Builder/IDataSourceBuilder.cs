namespace Deve.DataSource
{
    public interface IDataSourceBuilder
    {
        IDataSourceBuilder SetConfig(DataSourceConfig config);

        IDataSource Create(DataOptions? options = null);
    }
}