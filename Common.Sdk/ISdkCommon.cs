namespace Deve.Sdk
{
    /// <summary>
    /// Definitions available in all Sdk implementations (External & Internal).
    /// </summary>
    public interface ISdkCommon : IDataCommon
    {
        UserToken? UserToken { get; set; }
        HttpClient Client { get; }
    }
}
