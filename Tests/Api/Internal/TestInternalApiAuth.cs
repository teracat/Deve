using Deve.Internal.Api;

namespace Deve.Tests.Api.Internal
{
    /// <summary>
    /// Internal Api Auth endpoints tests.
    /// </summary>
    public class TestInternalApiAuth : TestApiBaseAuth<Program>, IClassFixture<FixtureApiInternal>
    {
        public TestInternalApiAuth(FixtureApiInternal fixture)
            : base(fixture)
        {
        }
    }
}