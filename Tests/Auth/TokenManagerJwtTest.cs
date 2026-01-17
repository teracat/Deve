using Deve.Tests.Auth.Fixtures;

namespace Deve.Tests.Auth;

/// <summary>
/// TokenManagerJwt Tests.
/// </summary>
public class TokenManagerJwtTest : TokenManagerBaseTest, IClassFixture<TokenManagerJwtFixture>
{
    public TokenManagerJwtTest(TokenManagerJwtFixture fixtureTokenManager)
        : base(fixtureTokenManager)
    {
    }
}
