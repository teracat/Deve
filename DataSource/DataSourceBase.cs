namespace Deve.DataSource
{
    internal abstract class DataSourceBase
    {
        #region Fields
        private readonly DataSourceMain _dataSourceMain;
        #endregion

        #region Properties
        protected DataSourceMain DataSourceMain => _dataSourceMain;
        #endregion

        #region Constructor
        public DataSourceBase(DataSourceMain dataSourceMain)
        {
            _dataSourceMain = dataSourceMain;
        }
        #endregion
    }
}
