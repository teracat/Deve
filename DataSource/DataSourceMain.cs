using Deve.Dto;
using Deve.Data;
using Deve.DataSource.Config;
using Deve.Internal.Dto;
using Deve.Internal.Data;

namespace Deve.DataSource
{
    /// <summary>
    /// DataSource sample that uses in-memory data.
    /// </summary>
    public class DataSourceMain : IDataSource
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
        public IDataSourceConfig? Config { get; }
        public IDataOptions Options { get; set; }
        #endregion

        #region IDataSource
        public IDataAll<Country, Country, CriteriaCountry> Countries => _dsCountry ??= new DataSourceCountry(this);
        public IDataAll<State, State, CriteriaState> States => _dsState ??= new DataSourceState(this);
        public IDataAll<City, City, CriteriaCity> Cities => _dsCity ??= new DataSourceCity(this);
        public IDataClient Clients => _dsClient ??= new DataSourceClient(this);
        public IDataAll<User, User, CriteriaUser> Users => _dsUser ??= new DataSourceUser(this);

        public External.Data.IDataGet<ClientBasic, External.Dto.Client, CriteriaClientBasic> ClientsBasic => _dsClientBasic ??= new DataSourceClientBasic(this);
        #endregion

        #region Constructor
        public DataSourceMain(IDataSourceConfig config, IDataOptions? options)
        {
            Config = config;   //The Config is not used in this implementation
            Options = options ?? new DataOptions();
        }

        public DataSourceMain(IDataSourceConfig config)
        {
            Config = config;   //The Config is not used in this implementation
            Options = new DataOptions();
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