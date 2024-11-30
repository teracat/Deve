using Deve.Internal.Api;

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