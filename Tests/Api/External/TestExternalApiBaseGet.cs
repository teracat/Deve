using Deve.External.Api;
using Deve.Tests.Api.External.Fixture;

namespace Deve.Tests.Api.External
{
    public abstract class TestExternalApiBaseGet : TestApiBaseGet<Program>
    {
        protected TestExternalApiBaseGet(FixtureApiExternal fixture)
            : base(fixture)
        {
        }
    }
}