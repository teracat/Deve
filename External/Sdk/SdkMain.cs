using Deve.Data;
using Deve.External.Data;
using Deve.Sdk;
using Deve.Sdk.LoggingHandlers;

namespace Deve.External.Sdk
{
    public class SdkMain : SdkMainBase, ISdk
    {
        #region Fields
        private IDataCountry? _sdkCountry;
        private IDataState? _sdkState;
        private IDataCity? _sdkCity;
        private IDataClientBasic? _sdkClient;
        #endregion

        #region Properties
        internal override string Url => _environment == EnvironmentType.Staging ? ApiConstants.UrlStagingExternal : ApiConstants.UrlProductionExternal;
        #endregion

        #region Constructor
        public SdkMain(EnvironmentType environment, DataOptions options, LoggingHandlerBase handler)
            : base(environment, options, handler)
        {
        }

        public SdkMain(EnvironmentType environment, LoggingHandlerBase handler)
            : base(environment, handler)
        {
        }

        public SdkMain(EnvironmentType environment, DataOptions options)
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

        public IDataClientBasic Clients => _sdkClient ??= new SdkClient(this);
        #endregion
    }
}
