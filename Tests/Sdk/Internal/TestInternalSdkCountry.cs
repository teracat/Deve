using Deve.Internal;
using Deve.Internal.Sdk;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkCountry : TestCountry<ISdk>, IClassFixture<FixtureDataSdk>, IClassFixture<FixtureDataSdkLogged>
    {
        public TestInternalSdkCountry(FixtureDataSdk fixtureData, FixtureDataSdkLogged fixtureDataLogged)
            : base(fixtureData, fixtureDataLogged)
        {
        }

        protected override IDataAll<Country, Country, CriteriaCountry> GetDataAll(ISdk sdk) => sdk.Countries;
    }
}