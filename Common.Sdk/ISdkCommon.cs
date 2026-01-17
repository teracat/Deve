using Deve.Data;
using Deve.Auth.Token;

namespace Deve.Sdk;

/// <summary>
/// Definitions available in all Sdk implementations (External & Internal).
/// </summary>
public interface ISdkCommon : IDisposable
{
    IDataOptions Options { get; }

    UserToken? UserToken { get; set; }

    HttpClient Client { get; }
}
