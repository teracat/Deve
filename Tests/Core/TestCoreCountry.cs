using Deve.Core;
using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreCountry : TestCountry<ICore>, IClassFixture<FixtureDataCore>
    {
        public TestCoreCountry(FixtureDataCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }

        protected override IDataAll<Country, Country, CriteriaCountry> GetDataAll(ICore core) => core.Countries;
    }
}