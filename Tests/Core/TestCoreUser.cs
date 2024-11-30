using Deve.Core;

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