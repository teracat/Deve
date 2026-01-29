using Deve.Tests.Auth.Fixtures;

namespace Deve.Tests.Auth;

/// <summary>
/// TokenManagerCrypt Tests.
/// </summary>
public class TokenManagerCryptTest : TokenManagerBaseTest, IClassFixture<TokenManagerCryptFixture>
{
    public TokenManagerCryptTest(TokenManagerCryptFixture fixtureTokenManager)
        : base(fixtureTokenManager)
    {
    }
}
