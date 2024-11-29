using Deve.Internal;
using Deve.Core;

namespace Deve.Tests.Core
{
    public class TestCoreCity : TestCity<ICore>, IClassFixture<FixtureDataCore>, IClassFixture<FixtureDataCoreLogged>
    {
        public TestCoreCity(FixtureDataCore fixtureDataCore, FixtureDataCoreLogged fixtureDataLogged)
            : base(fixtureDataCore, fixtureDataLogged)
        {
        }

        protected override IDataAll<City, City, CriteriaCity> GetDataAll(ICore core) => core.Cities;
    }
}