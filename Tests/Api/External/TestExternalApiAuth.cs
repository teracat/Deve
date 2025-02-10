using Deve.External.Api;
using Deve.Tests.Api.External.Fixture;

namespace Deve.Tests.Api.External
{
    /// <summary>
    /// External Api Auth endpoints tests.
    /// </summary>
    public class TestExternalApiAuth : TestApiBaseAuth<Program>, IClassFixture<FixtureApiExternal>
    {
        public TestExternalApiAuth(FixtureApiExternal fixture)
            : base(fixture)
        {
        }
    }
}