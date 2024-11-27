using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreStats : TestStats
    {
        protected override IData CreateData() => TestsCoreHelpers.CreateCore();
    }
}