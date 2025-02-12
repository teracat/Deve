using Deve.Authenticate;
using Deve.Data;
using Deve.Sdk.LoggingHandlers;

namespace Deve.Sdk
{
    internal abstract class SdkMainBase : ISdkCommon
    {
        #region Fields
        protected readonly EnvironmentType _environment = EnvironmentType.Production;
        protected readonly HttpClient _client;
        protected DataOptions _options;

        private SdkAuth? _sdkAuth;
        #endregion

        #region ISdk
        public UserToken? UserToken { get; set; }
        #endregion

        #region IDataCommon
        public HttpClient Client => _client;
        public DataOptions Options
        {
            get => _options;
            set
            {
                if (_options != value)
                {
                    _options = value;
                    UpdatedOptions();
                }
            }
        }
        internal abstract string Url { get; }

        public IAuthenticate Authenticate => _sdkAuth ??= new SdkAuth(this);
        #endregion

        #region Constructor
        public SdkMainBase(EnvironmentType environment = EnvironmentType.Production, DataOptions? options = null, LoggingHandlerBase? handler = null)
        {
            _environment = environment;
            _options = options ?? new DataOptions();

            _client = handler is null ? new HttpClient() : new HttpClient(handler);
            _client.BaseAddress = new Uri(Url);

            UpdatedOptions();
        }

        internal SdkMainBase(HttpClient client, DataOptions? options = null)
        {
            _client = client;
            _options = options ?? new DataOptions();
        }
        #endregion

        #region Methods
        private void UpdatedOptions()
        {
            _client.DefaultRequestHeaders.AcceptLanguage.Clear();
            _client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue(_options.LangCode));
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _client.Dispose();
        }
        #endregion
    }
}