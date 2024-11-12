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
        private SdkBaseAll<Country, Country, CriteriaCountry>? _sdkCountry;
        private SdkBaseAll<State, State, CriteriaState>? _sdkState;
        private SdkBaseAll<City, City, CriteriaCity>? _sdkCity;
        private SdkClientAll? _sdkClient;
        private SdkBaseAll<UserBase, UserPlainPassword, CriteriaUser>? _sdkUser;
        private SdkStats? _sdkStats;
        private SdkBaseGet<ClientBasic, External.Client, CriteriaClientBasic, ISdkCommon>? _sdkClientBasicGet;
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
        public IDataAll<Country, Country, CriteriaCountry> Countries => _sdkCountry ??= new SdkBaseAll<Country, Country, CriteriaCountry>(ApiConstants.PathCountry, this);

        public IDataAll<State, State, CriteriaState> States => _sdkState ??= new SdkBaseAll<State, State, CriteriaState>(ApiConstants.PathState, this);

        public IDataAll<City, City, CriteriaCity> Cities => _sdkCity ??= new SdkBaseAll<City, City, CriteriaCity>(ApiConstants.PathCity, this);

        public IDataClient Clients => _sdkClient ??= new SdkClientAll(this);

        public External.IDataGet<ClientBasic, External.Client, CriteriaClientBasic> ClientsBasic => _sdkClientBasicGet ??= new SdkBaseGet<ClientBasic, External.Client, CriteriaClientBasic, ISdkCommon>(ApiConstants.PathClientBasic, this);

        public IDataAll<UserBase, UserPlainPassword, CriteriaUser> Users => _sdkUser ??= new SdkBaseAll<UserBase, UserPlainPassword, CriteriaUser>(ApiConstants.PathUser, this);

        public IDataStats Stats => _sdkStats ??= new SdkStats(this);
        #endregion
    }
}
