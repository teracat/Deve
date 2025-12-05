using Deve.Data;
using Deve.External.Data;
using Deve.Sdk;

namespace Deve.External.Sdk
{
    public class SdkMain : SdkMainBase, ISdk
    {
        #region Properties
        internal override string Url => _environment == EnvironmentType.Staging ? ApiConstants.UrlStagingExternal : ApiConstants.UrlProductionExternal;
        #endregion

        #region Constructor
        public SdkMain(EnvironmentType environment, DataOptions options, HttpMessageHandler handler)
            : base(environment, options, handler)
        {
        }

        public SdkMain(EnvironmentType environment, HttpMessageHandler handler)
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
        public IDataCountry Countries => field ??= new SdkCountry(this);

        public IDataState States => field ??= new SdkState(this);

        public IDataCity Cities => field ??= new SdkCity(this);

        public IDataClientBasic Clients => field ??= new SdkClient(this);
        #endregion
    }
}
