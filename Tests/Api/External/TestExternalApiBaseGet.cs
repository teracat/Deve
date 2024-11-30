using Deve.External.Api;

namespace Deve.Tests.Api.External
{
    public abstract class TestExternalApiBaseGet : TestApiBaseGet<Program>
    {
        public TestExternalApiBaseGet(FixtureApiExternal fixture)
            : base(fixture)
        {
        }
    }
}