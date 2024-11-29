using Deve.Core;
using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreCountry : TestCountry<ICore>, IClassFixture<FixtureDataCore>, IClassFixture<FixtureDataCoreLogged>
    {
        public TestCoreCountry(FixtureDataCore fixtureDataCore, FixtureDataCoreLogged fixtureDataLogged)
            : base(fixtureDataCore, fixtureDataLogged)
        {
        }

        protected override IDataAll<Country, Country, CriteriaCountry> GetDataAll(ICore core) => core.Countries;
    }
}