namespace Deve.Tests.Api.External
{
    public class TestExternalApiCountry : TestExternalApiBaseGet, IClassFixture<FixtureApiExternal>
    {
        protected override string Path => ApiConstants.PathCountry;

        public TestExternalApiCountry(FixtureApiExternal fixture)
            : base(fixture)
        {
        }
    }
}