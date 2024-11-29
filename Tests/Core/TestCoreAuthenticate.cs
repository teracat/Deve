using Deve.Core;

namespace Deve.Tests.Core
{
    /// <summary>
    /// Authenticate Tests for Core.
    /// </summary>
    public class TestCoreAuthenticate : TestAuthenticate<ICore>, IClassFixture<FixtureDataCore>, IClassFixture<FixtureDataCoreLogged>
    {
        public TestCoreAuthenticate(FixtureDataCore fixtureDataCore, FixtureDataCoreLogged fixtureDataLogged)
            : base(fixtureDataCore, fixtureDataLogged)
        {
        }
    }
}