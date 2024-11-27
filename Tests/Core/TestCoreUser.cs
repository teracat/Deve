using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreUser : TestUser
    {
        protected override IData CreateData() => TestsCoreHelpers.CreateCore();
    }
}