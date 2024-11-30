namespace Deve.Tests.Api.External
{
    public class TestExternalApiClient : TestExternalApiBaseGet, IClassFixture<FixtureApiExternal>
    {
        protected override string Path => ApiConstants.PathClient;

        public TestExternalApiClient(FixtureApiExternal fixture)
            : base(fixture)
        {
        }
    }
}