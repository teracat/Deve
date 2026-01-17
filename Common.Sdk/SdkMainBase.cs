using Deve.Auth.Token;
using Deve.Data;

namespace Deve.Sdk;

public abstract class SdkMainBase : ISdkCommon
{
    #region Properties
    protected EnvironmentType Environment { get; } = EnvironmentType.Production;
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
    internal abstract string Url { get; }
    #endregion

    #region Constructors
    protected SdkMainBase(EnvironmentType environment, IDataOptions options, HttpMessageHandler handler)
    {
        Environment = environment;
        Options = options;
        Client = new HttpClient(handler);
        UpdatedOptions();
    }

    protected SdkMainBase(EnvironmentType environment, HttpMessageHandler handler)
    {
        Environment = environment;
        Options = new DataOptions();
        Client = new HttpClient(handler);
        UpdatedOptions();
    }

    protected SdkMainBase(EnvironmentType environment, IDataOptions options)
    {
        Environment = environment;
        Options = options;
        Client = new HttpClient();
        UpdatedOptions();
    }

    protected SdkMainBase(EnvironmentType environment)
    {
        Environment = environment;
        Options = new DataOptions();
        Client = new HttpClient();
        UpdatedOptions();
    }

    protected SdkMainBase(HttpClient client, DataOptions options)
    {
        Client = client;
        Options = options;
    }

    protected SdkMainBase(HttpClient client)
    {
        Client = client;
        Options = new DataOptions();
    }
    #endregion

    #region Methods
    private void UpdatedOptions()
    {
        Client.BaseAddress = new Uri(Url);
        Client.DefaultRequestHeaders.AcceptLanguage.Clear();
        Client.DefaultRequestHeaders.AcceptLanguage.Add(new System.Net.Http.Headers.StringWithQualityHeaderValue(Options.LangCode));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        Client.Dispose();
    }
    #endregion

    #region IDisposable
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
