using Deve.DataSource;

namespace Deve.Tests
{
    public class DataSourceMockFactory : IDataSourceFactory
    {
        #region IDataSourceFactory
        public void UseConfig(DataSourceConfig config)
        {
        }

        public IDataSource Create(DataOptions? options = null) => TestsHelpers.CreateDataSource();
        #endregion
    }
}