using Deve.DataSource;

namespace Deve.Core.DataSourceWrappers
{
    internal class DataSourceWrapperBase
    {
        protected ICore Core { get; }
        protected IDataSource Source => Core.DataSource;

        public DataSourceWrapperBase(ICore core)
        {
            Core = core;
        }
    }
}