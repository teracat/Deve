using Deve.Sdk;
using Deve.Model;
using Deve.Data;
using Deve.Sdk.LoggingHandlers;
using Deve.Criteria;
using Deve.External.Sdk;
using Deve.Internal.Model;
using Deve.Internal.Data;
using Deve.Internal.Criteria;

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
        private SdkBaseGet<ClientBasic, External.Model.Client, CriteriaClientBasic, ISdkCommon>? _sdkClientBasicGet;
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

        internal SdkMain(HttpClient client, DataOptions? options = null)
            : base(client, options)
        {
        }
        #endregion

        #region IData
        public IDataAll<Country, Country, CriteriaCountry> Countries => _sdkCountry ??= new SdkBaseAll<Country, Country, CriteriaCountry>(ApiConstants.PathCountry, this);

        public IDataAll<State, State, CriteriaState> States => _sdkState ??= new SdkBaseAll<State, State, CriteriaState>(ApiConstants.PathState, this);

        public IDataAll<City, City, CriteriaCity> Cities => _sdkCity ??= new SdkBaseAll<City, City, CriteriaCity>(ApiConstants.PathCity, this);

        public IDataClient Clients => _sdkClient ??= new SdkClientAll(this);

        public External.Data.IDataGet<ClientBasic, External.Model.Client, CriteriaClientBasic> ClientsBasic => _sdkClientBasicGet ??= new SdkBaseGet<ClientBasic, External.Model.Client, CriteriaClientBasic, ISdkCommon>(ApiConstants.PathClientBasic, this);

        public IDataAll<UserBase, UserPlainPassword, CriteriaUser> Users => _sdkUser ??= new SdkBaseAll<UserBase, UserPlainPassword, CriteriaUser>(ApiConstants.PathUser, this);

        public IDataStats Stats => _sdkStats ??= new SdkStats(this);
        #endregion
    }
}
