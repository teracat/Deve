using Deve.Core;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public class TestCoreUser : TestUser<ICore>, IClassFixture<FixtureCore>
    {
        public TestCoreUser(FixtureCore fixtureDataCore)
           : base(fixtureDataCore)
        {
        }
    }
}