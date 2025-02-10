using Deve.Internal.Api;
using Deve.Tests.Api.Internal.Fixture;

namespace Deve.Tests.Api.Internal
{
    /// <summary>
    /// Internal Api tests base class.
    /// </summary>
    public class TestInternalApiBase : TestApiBase<Program>
    {
        public TestInternalApiBase(FixtureApiInternal fixture)
            : base(fixture)
        {
        }
    }
}