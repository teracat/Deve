using Deve.Authenticate;
using Deve.Data;

namespace Deve.Sdk
{
    public abstract class SdkMainBase : ISdkCommon
    {
        #region Fields
        protected readonly EnvironmentType _environment = EnvironmentType.Production;
        protected readonly HttpClient _client;
        protected IDataOptions _options;

        private SdkAuth? _sdkAuth;
        #endregion

        #region ISdk
        public UserToken? UserToken { get; set; }
        #endregion

        #region IDataCommon
        public HttpClient Client => _client;
        public IDataOptions Options
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

        #region Constructors
        protected SdkMainBase(EnvironmentType environment, IDataOptions options, HttpMessageHandler handler)
        {
            _environment = environment;
            _options = options;
            _client = new HttpClient(handler);
            UpdatedOptions();
        }

        protected SdkMainBase(EnvironmentType environment, HttpMessageHandler handler)
        {
            _environment = environment;
            _options = new DataOptions();
            _client = new HttpClient(handler);
            UpdatedOptions();
        }

        protected SdkMainBase(EnvironmentType environment, IDataOptions options)
        {
            _environment = environment;
            _options = options;
            _client = new HttpClient();
            UpdatedOptions();
        }

        protected SdkMainBase(EnvironmentType environment)
        {
            _environment = environment;
            _options = new DataOptions();
            _client = new HttpClient();
            UpdatedOptions();
        }

        protected SdkMainBase(HttpClient client, DataOptions options)
        {
            _client = client;
            _options = options;
        }

        protected SdkMainBase(HttpClient client)
        {
            _client = client;
            _options = new DataOptions();
        }
        #endregion

        #region Methods
        private void UpdatedOptions()
        {
            _client.BaseAddress = new Uri(Url);
            _client.DefaultRequestHeaders.AcceptLanguage.Clear();
            _client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue(_options.LangCode));
        }
        #endregion

        #region IDisposable
        public void Dispose() => _client.Dispose();
        #endregion
    }
}