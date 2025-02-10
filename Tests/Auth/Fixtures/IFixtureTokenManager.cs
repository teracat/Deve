using Deve.Auth.TokenManagers;

namespace Deve.Tests.Auth.Fixtures
{
    public interface IFixtureTokenManager
    {
        ITokenManager TokenManager { get; }
        string TokenExpired { get; }
    }
}