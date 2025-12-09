using Deve.Core;
using Deve.Criteria;
using Deve.Internal.Data;
using Deve.Model;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public class TestCoreCity : TestCity<ICore>, IClassFixture<FixtureCore>
    {
        public TestCoreCity(FixtureCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }

        protected override IDataAll<City, City, CriteriaCity> GetDataAll(ICore core) => core.Cities;
    }
}