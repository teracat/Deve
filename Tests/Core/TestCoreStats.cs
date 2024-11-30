using Deve.Core;

namespace Deve.Tests.Core
{
    public class TestCoreStats : TestStats<ICore>, IClassFixture<FixtureDataCore>
    {
        public TestCoreStats(FixtureDataCore fixtureDataCore)
           : base(fixtureDataCore)
        {
        }
    }
}