using Deve.Auth;

namespace Deve.Tests.Auth
{
    public interface IFixtureTokenManager
    {
        ITokenManager TokenManager { get; }
        string TokenExpired { get; }
    }
}
