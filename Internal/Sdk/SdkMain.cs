using Deve.Data;
using Deve.Internal.Data;
using Deve.Sdk;

namespace Deve.Internal.Sdk
{
    /// <summary>
    /// Internal Sdk implementation.
    /// </summary>
    public class SdkMain : SdkMainBase, ISdk
    {
        #region Properties
        internal override string Url => _environment == EnvironmentType.Staging ? ApiInternalConstants.UrlStaging : ApiInternalConstants.UrlProduction;
        #endregion

        #region Constructor
        public SdkMain(EnvironmentType environment, IDataOptions options, HttpMessageHandler handler)
            : base(environment, options, handler)
        {
        }

        public SdkMain(EnvironmentType environment, HttpMessageHandler handler)
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
        public IDataCountry Countries => field ??= new SdkCountry(this);

        public IDataState States => field ??= new SdkState(this);

        public IDataCity Cities => field ??= new SdkCity(this);

        public IDataClient Clients => field ??= new SdkClientAll(this);

        public External.Data.IDataClientBasic ClientsBasic => field ??= new SdkClientBasic(this);

        public IDataUser Users => field ??= new SdkUser(this);

        public IDataStats Stats => field ??= new SdkStats(this);
        #endregion
    }
}