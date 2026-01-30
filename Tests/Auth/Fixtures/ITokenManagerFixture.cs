using Deve.Auth.TokenManagers;

namespace Deve.Tests.Auth.Fixtures;

public interface ITokenManagerFixture
{
    ITokenManager TokenManager { get; }
    string TokenExpired { get; }
}
