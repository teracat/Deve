using Deve.Sdk;
using Deve.External.Sdk;

namespace Deve.Internal.Sdk
{
    /// <summary>
    /// Internal Sdk implementation.
    /// </summary>
    internal class SdkMain : SdkMainBase, ISdk
    {
        #region Fields
        private SdkCountryAll? _sdkCountry;
        private SdkStateAll? _sdkState;
        private SdkCityAll? _sdkCity;
        private SdkClientAll? _sdkClient;
        private SdkUserAll? _sdkUser;
        private SdkStats? _sdkStats;
        private SdkClientBasicGet? _sdkClientBasicGet;
        #endregion

        #region Properties
        internal override string Url
        {
            get
            {
                switch (_environment)
                {
                    case EnvironmentType.Staging:
                        return ApiInternalConstants.UrlStaging;
                    default:
                        return ApiInternalConstants.UrlProduction;
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
        public IDataAll<Country, Country, CriteriaCountry> Countries => _sdkCountry ??= new SdkCountryAll(this);

        public IDataAll<State, State, CriteriaState> States => _sdkState ??= new SdkStateAll(this);

        public IDataAll<City, City, CriteriaCity> Cities => _sdkCity ??= new SdkCityAll(this);

        public IDataClient Clients => _sdkClient ??= new SdkClientAll(this);

        public External.IDataGet<ClientBasic, External.Client, CriteriaClientBasic> ClientsBasic => _sdkClientBasicGet ??= new SdkClientBasicGet(this, ApiConstants.ApiPathClientBasic);

        public IDataAll<User, User, CriteriaUser> Users => _sdkUser ??= new SdkUserAll(this);

        public IDataStats Stats => _sdkStats ??= new SdkStats(this);
        #endregion
    }
}
