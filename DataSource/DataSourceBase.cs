namespace Deve.DataSource
{
    internal abstract class DataSourceBase
    {
        #region Fields
        private readonly IDataSource _dataSource;
        #endregion

        #region Properties
        protected IDataSource DataSource => _dataSource;
        #endregion

        #region Constructor
        protected DataSourceBase(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        #endregion
    }
}