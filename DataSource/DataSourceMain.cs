using Deve.Criteria;
using Deve.Data;
using Deve.DataSource.Config;
using Deve.Internal.Criteria;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Deve.Model;

namespace Deve.DataSource
{
    internal class DataSourceMain : IDataSource
    {
        #region Fields
        private DataSourceCountry? _dsCountry;
        private DataSourceState? _dsState;
        private DataSourceCity? _dsCity;
        private DataSourceClient? _dsClient;
        private DataSourceClientBasic? _dsClientBasic;
        private DataSourceUser? _dsUser;
        #endregion

        #region Properties
        public DataSourceConfig? Config { get; }
        public DataOptions Options { get; set; }
        #endregion

        #region IDataSource
        public IDataAll<Country, Country, CriteriaCountry> Countries => _dsCountry ??= new DataSourceCountry(this);
        public IDataAll<State, State, CriteriaState> States => _dsState ??= new DataSourceState(this);
        public IDataAll<City, City, CriteriaCity> Cities => _dsCity ??= new DataSourceCity(this);
        public IDataClient Clients => _dsClient ??= new DataSourceClient(this);
        public IDataAll<User, User, CriteriaUser> Users => _dsUser ??= new DataSourceUser(this);

        public External.Data.IDataGet<ClientBasic, External.Model.Client, CriteriaClientBasic> ClientsBasic => _dsClientBasic ??= new DataSourceClientBasic(this);
        #endregion

        #region Constructor
        public DataSourceMain(DataSourceConfig? config = null, DataOptions? options = null)
        {
            Config = config;   //The Config is not used in this implementation
            Options = options ?? new DataOptions();
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            // Nothing to be disposed in this implementation
            // You should dispose any database connection or file stream here
        }
        #endregion
    }
}