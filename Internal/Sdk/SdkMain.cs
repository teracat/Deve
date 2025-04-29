using Deve.Sdk;
using Deve.Data;
using Deve.Sdk.LoggingHandlers;
using Deve.Internal.Data;

namespace Deve.Internal.Sdk
{
    /// <summary>
    /// Internal Sdk implementation.
    /// </summary>
    public class SdkMain : SdkMainBase, ISdk
    {
        #region Fields
        private IDataCountry? _sdkCountry;
        private IDataState? _sdkState;
        private IDataCity? _sdkCity;
        private IDataClient? _sdkClient;
        private IDataUser? _sdkUser;
        private IDataStats? _sdkStats;
        private External.Data.IDataClientBasic? _sdkClientBasicGet;
        #endregion

        #region Properties
        internal override string Url => _environment == EnvironmentType.Staging ? ApiInternalConstants.UrlStaging : ApiInternalConstants.UrlProduction;
        #endregion

        #region Constructor
        public SdkMain(EnvironmentType environment, IDataOptions options, LoggingHandlerBase handler)
            : base(environment, options, handler)
        {
        }

        public SdkMain(EnvironmentType environment, LoggingHandlerBase handler)
            : base(environment, handler)
        {
        }

        public SdkMain(EnvironmentType environment, IDataOptions options)
            : base(environment, options)
        {
        }

        public SdkMain(EnvironmentType environment)
            : base(environment)
        {
        }

        internal SdkMain(HttpClient client, DataOptions options)
            : base(client, options)
        {
        }

        internal SdkMain(HttpClient client)
            : base(client)
        {
        }
        #endregion

        #region IData
        public IDataCountry Countries => _sdkCountry ??= new SdkCountry(this);

        public IDataState States => _sdkState ??= new SdkState(this);

        public IDataCity Cities => _sdkCity ??= new SdkCity(this);

        public IDataClient Clients => _sdkClient ??= new SdkClientAll(this);

        public External.Data.IDataClientBasic ClientsBasic => _sdkClientBasicGet ??= new SdkClientBasic(this);

        public IDataUser Users => _sdkUser ??= new SdkUser(this);

        public IDataStats Stats => _sdkStats ??= new SdkStats(this);
        #endregion
    }
}