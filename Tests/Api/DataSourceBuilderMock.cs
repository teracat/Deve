using Deve.DataSource;

namespace Deve.Tests.Api
{
    /// <summary>
    /// IDataSourceBuilder implementatin that uses DataSourceMock from Tests.Common
    /// </summary>
    public class DataSourceBuilderMock : IDataSourceBuilder
    {
        public IDataSourceBuilder SetConfig(DataSourceConfig config) => this;

        public IDataSource Create(DataOptions? options = null) => TestsHelpers.CreateDataSourceMock();
    }
}