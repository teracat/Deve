using Deve.Core;

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