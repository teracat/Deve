using Deve.Core;
using Deve.Dto;
using Deve.Internal.Data;
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