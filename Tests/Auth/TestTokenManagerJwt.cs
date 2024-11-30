namespace Deve.Tests.Auth
{
    /// <summary>
    /// TokenManagerJwt Tests.
    /// </summary>
    public class TestTokenManagerJwt : TestTokenManagerBase, IClassFixture<FixtureTokenManagerJwt>
    {
        public TestTokenManagerJwt(FixtureTokenManagerJwt fixtureTokenManager)
            : base(fixtureTokenManager)
        {
        }
    }
}
