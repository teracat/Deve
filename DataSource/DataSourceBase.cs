namespace Deve.DataSource
{
    internal abstract class DataSourceBase
    {
        #region Properties
        protected IDataSource DataSource { get; }
        #endregion

        #region Constructor
        protected DataSourceBase(IDataSource dataSource)
        {
            DataSource = dataSource;
        }
        #endregion
    }
}