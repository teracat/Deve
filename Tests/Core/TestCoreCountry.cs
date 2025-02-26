using Deve.Core;
using Deve.Criteria;
using Deve.Model;
using Deve.Internal.Data;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public class TestCoreCountry : TestCountry<ICore>, IClassFixture<FixtureCore>
    {
        public TestCoreCountry(FixtureCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }

        protected override IDataAll<Country, Country, CriteriaCountry> GetDataAll(ICore core) => core.Countries;
    }
}