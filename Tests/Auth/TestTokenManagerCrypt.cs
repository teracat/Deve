using Deve.Tests.Auth.Fixtures;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// TokenManagerCrypt Tests.
    /// </summary>
    public class TestTokenManagerCrypt : TestTokenManagerBase, IClassFixture<FixtureTokenManagerCrypt>
    {
        public TestTokenManagerCrypt(FixtureTokenManagerCrypt fixtureTokenManager)
            : base(fixtureTokenManager)
        {
        }
    }
}