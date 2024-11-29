using Deve.Core;

namespace Deve.Tests.Core
{
    public class TestCoreStats : TestStats<ICore>, IClassFixture<FixtureDataCore>, IClassFixture<FixtureDataCoreLogged>
    {
        public TestCoreStats(FixtureDataCore fixtureDataCore, FixtureDataCoreLogged fixtureDataLogged)
           : base(fixtureDataCore, fixtureDataLogged)
        {
        }
    }
}