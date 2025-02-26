using Deve.Core;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public class TestCoreClient : TestClient<ICore>, IClassFixture<FixtureCore>
    {
        public TestCoreClient(FixtureCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }
    }
}