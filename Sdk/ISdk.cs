using Deve.Data;
using Deve.Auth;

namespace Deve.Sdk;

public interface ISdk : IData
{
    IDataOptions Options { get; }

    UserToken? UserToken { get; set; }

    HttpClient Client { get; }
}
