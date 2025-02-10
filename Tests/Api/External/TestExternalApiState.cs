using Deve.Tests.Api.External.Fixture;

namespace Deve.Tests.Api.External
{
    public class TestExternalApiState : TestExternalApiBaseGet, IClassFixture<FixtureApiExternal>
    {
        protected override string Path => ApiConstants.PathState;

        public TestExternalApiState(FixtureApiExternal fixture)
            : base(fixture)
        {
        }
    }
}