using Deve.Data;
// <hooks:sdk-main-using>
using Deve.Auth;
using Deve.Customers;
using Deve.Identity;
// <hooks:sdk-main-using-sdks>
using Deve.Sdk.Auth;
using Deve.Sdk.Customers;
using Deve.Sdk.Identity;

namespace Deve.Sdk;

internal sealed class MainSdk : ISdk
{
    #region Properties
    public EnvironmentType Environment { get; } = EnvironmentType.Production;

    public UserToken? UserToken { get; set; }

    public HttpClient Client { get; }

    public IDataOptions Options
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                UpdatedOptions();
            }
        }
    }

    public Uri Url => Environment == EnvironmentType.Staging ? SdkConstants.UrlStaging : SdkConstants.UrlProduction;
    #endregion

    #region Constructor
    public MainSdk(EnvironmentType environment, IDataOptions options, HttpMessageHandler handler)
    {
        Client = new HttpClient(handler);
        Environment = environment;
        Options = options;
    }

    public MainSdk(EnvironmentType environment, IDataOptions options)
    {
        Client = new HttpClient();
        Environment = environment;
        Options = options;
    }

    public MainSdk(HttpClient client, DataOptions options)
    {
        Client = client;
        Options = options;
    }
    #endregion

    #region Methods
    private void UpdatedOptions()
    {
        Client.BaseAddress = Url;
        Client.DefaultRequestHeaders.AcceptLanguage.Clear();
        Client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue(Options.LangCode));
    }
    #endregion

    #region IData
    // <hooks:sdk-main-properties>

    public IAuthData Auth => field ??= new AuthSdk(this);

    public ICustomersData Customers => field ??= new CustomersSdk(this);

    public IIdentityData Identity => field ??= new IdentitySdk(this);
    #endregion

    #region IDisposable
    public void Dispose() => Client.Dispose();
    #endregion
}
