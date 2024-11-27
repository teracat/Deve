using Deve.Core;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBase : TestBase<ICore>
    {
        protected override ICore CreateData() => TestsCoreHelpers.CreateCore();
    }
}