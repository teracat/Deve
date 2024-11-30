using Deve.External.Api;

namespace Deve.Tests.Api.External
{
    /// <summary>
    /// External Api tests base class.
    /// </summary>
    public abstract class TestExternalApiBase : TestApiBase<Program>
    {
        public TestExternalApiBase(FixtureApiExternal fixture)
            : base(fixture)
        {
        }
    }
}