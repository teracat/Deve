using Deve.Core;

namespace Deve.Tests.Core
{
    public class TestCoreUser : TestUser<ICore>, IClassFixture<FixtureDataCore>, IClassFixture<FixtureDataCoreLogged>
    {
        public TestCoreUser(FixtureDataCore fixtureDataCore, FixtureDataCoreLogged fixtureDataLogged)
           : base(fixtureDataCore, fixtureDataLogged)
        {
        }
    }
}