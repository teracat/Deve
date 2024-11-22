namespace Deve.DataSource
{
    public interface IDataSourceFactory
    {
        void UseConfig(DataSourceConfig config);

        IDataSource Create(DataOptions? options = null);
    }
}