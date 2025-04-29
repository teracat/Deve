using Deve.DataSource;

namespace Deve.Core.DataSourceWrappers
{
    internal class DataSourceWrapperBase
    {
        protected IDataSource Source { get; }

        public DataSourceWrapperBase(IDataSource dataSource)
        {
            Source = dataSource;
        }
    }
}