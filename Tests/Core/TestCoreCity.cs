using Deve.Criteria;
using Deve.Internal.Data;
using Deve.Model;
using Deve.Core;
using Deve.Tests.Core.Fixtures;

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