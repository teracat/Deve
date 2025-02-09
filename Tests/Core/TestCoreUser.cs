using Deve.Core;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public class TestCoreUser : TestUser<ICore>, IClassFixture<FixtureDataCore>
    {
        public TestCoreUser(FixtureDataCore fixtureDataCore)
           : base(fixtureDataCore)
        {
        }
    }
}