using Deve.Criteria;
using Deve.Data;
using Deve.Model;
using Deve.Sdk;
using Deve.Sdk.LoggingHandlers;
using Deve.External.Model;
using Deve.External.Data;

namespace Deve.External.Sdk
{
    internal class SdkMain : SdkMainBase, ISdk
    {
        #region Fields
        private SdkBaseGet<Country, Country, CriteriaCountry, ISdk>? _sdkCountry;
        private SdkBaseGet<State, State, CriteriaState, ISdk>? _sdkState;
        private SdkBaseGet<City, City, CriteriaCity, ISdk>? _sdkCity;
        private SdkBaseGet<ClientBasic, Client, CriteriaClientBasic, ISdkCommon>? _sdkClient;
        #endregion

        #region Properties
        internal override string Url => _environment == EnvironmentType.Staging ? ApiConstants.UrlStagingExternal : ApiConstants.UrlProductionExternal;
        #endregion

        #region Constructor
        public SdkMain(EnvironmentType environment = EnvironmentType.Production, DataOptions? options = null, LoggingHandlerBase? handler = null)
            : base(environment, options, handler)
        {
        }

        internal SdkMain(HttpClient client, DataOptions? options = null)
            : base(client, options)
        {
        }
        #endregion

        #region IData
        public IDataGet<Country, Country, CriteriaCountry> Countries => _sdkCountry ??= new SdkBaseGet<Country, Country, CriteriaCountry, ISdk>(ApiConstants.PathCountry, this);

        public IDataGet<State, State, CriteriaState> States => _sdkState ??= new SdkBaseGet<State, State, CriteriaState, ISdk>(ApiConstants.PathState, this);

        public IDataGet<City, City, CriteriaCity> Cities => _sdkCity ??= new SdkBaseGet<City, City, CriteriaCity, ISdk>(ApiConstants.PathCity, this);

        public IDataGet<ClientBasic, Client, CriteriaClientBasic> Clients => _sdkClient ??= new SdkBaseGet<ClientBasic, Client, CriteriaClientBasic, ISdkCommon>(ApiConstants.PathClient, this);
        #endregion
    }
}