namespace Deve.Tests.Api.External
{
    public class TestExternalApiCity : TestExternalApiBaseGet, IClassFixture<FixtureApiExternal>
    {
        protected override string Path => ApiConstants.PathCity;

        public TestExternalApiCity(FixtureApiExternal fixture)
            : base(fixture)
        {
        }
    }
}