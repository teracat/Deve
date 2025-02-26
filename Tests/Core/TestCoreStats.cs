using Deve.Core;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public class TestCoreStats : TestStats<ICore>, IClassFixture<FixtureCore>
    {
        public TestCoreStats(FixtureCore fixtureDataCore)
           : base(fixtureDataCore)
        {
        }
    }
}