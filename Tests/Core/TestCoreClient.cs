using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreClient : TestClient
    {
        protected override IData CreateData() => TestsCoreHelpers.CreateCore();
    }
}