using Deve.External;
using Deve.External.Sdk;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkCountry : TestBaseDataGet<ISdk, Country, Country, CriteriaCountry>, IClassFixture<FixtureDataSdk>, IClassFixture<FixtureDataSdkLogged>
    {
        public TestExternalSdkCountry(FixtureDataSdk fixtureData, FixtureDataSdkLogged fixtureDataLogged)
            : base(fixtureData, fixtureDataLogged)
        {
        }

        protected override IDataGet<Country, Country, CriteriaCountry> GetDataGet(ISdk sdk) => sdk.Countries;
    }
}