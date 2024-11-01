using Deve.Sdk;

namespace Deve.External.Sdk
{
    internal class SdkMain : SdkMainBase, ISdk
    {
        #region Fields
        private SdkCountryGet? _sdkCountry;
        private SdkStateGet? _sdkState;
        private SdkCityGet? _sdkCity;
        private SdkClientBasicGet? _sdkClient;
        #endregion

        #region Properties
        internal override string Url
        {
            get
            {
                switch (_environment)
                {
                    case EnvironmentType.Staging:
                        return ApiConstants.UrlStagingExternal;
                    default:
                        return ApiConstants.UrlProductionExternal;
                }
            }
        }
        #endregion

        #region Constructor
        public SdkMain(EnvironmentType environment = EnvironmentType.Production, DataOptions? options = null, LoggingHandlerBase? handler = null)
            : base(environment, options, handler)
        {
        }
        #endregion

        #region IData
        public IDataGet<Country, Country, CriteriaCountry> Countries => _sdkCountry ??= new SdkCountryGet(this);

        public IDataGet<State, State, CriteriaState> States => _sdkState ??= new SdkStateGet(this);

        public IDataGet<City, City, CriteriaCity> Cities => _sdkCity ??= new SdkCityGet(this);

        public IDataGet<ClientBasic, Client, CriteriaClientBasic> Clients => _sdkClient ??= new SdkClientBasicGet(this, ApiConstants.ApiPathClient);
        #endregion
    }
}
