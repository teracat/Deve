using Deve.DataSource;

namespace Deve.Core.DataSourceWrappers
{
    internal class DataSourceWrapperBase
    {
        protected CoreMain Core { get; }
        protected IDataSource Source => Core.DataSource;

        public DataSourceWrapperBase(CoreMain core)
        {
            Core = core;
        }
    }
}
