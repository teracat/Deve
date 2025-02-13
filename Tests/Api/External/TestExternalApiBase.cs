using Deve.External.Api;
using Deve.Tests.Api.External.Fixture;

namespace Deve.Tests.Api.External
{
    /// <summary>
    /// External Api tests base class.
    /// </summary>
    public abstract class TestExternalApiBase : TestApiBase<Program>
    {
        protected TestExternalApiBase(FixtureApiExternal fixture)
            : base(fixture)
        {
        }
    }
}