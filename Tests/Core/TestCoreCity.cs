using Deve.Internal;
using Deve.Core;

namespace Deve.Tests.Core
{
    public class TestCoreCity : TestCity<ICore>, IClassFixture<FixtureDataCore>
    {
        public TestCoreCity(FixtureDataCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }

        protected override IDataAll<City, City, CriteriaCity> GetDataAll(ICore core) => core.Cities;
    }
}