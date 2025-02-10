using Deve.Core;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public class TestCoreClient : TestClient<ICore>, IClassFixture<FixtureDataCore>
    {
        public TestCoreClient(FixtureDataCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }
    }
}