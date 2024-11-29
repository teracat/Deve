using Deve.External;
using Deve.External.Sdk;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkCity : TestBaseDataGet<ISdk, City, City, CriteriaCity>, IClassFixture<FixtureDataSdk>, IClassFixture<FixtureDataSdkLogged>
    {
        public TestExternalSdkCity(FixtureDataSdk fixtureData, FixtureDataSdkLogged fixtureDataLogged)
            : base(fixtureData, fixtureDataLogged)
        {
        }

        protected override IDataGet<City, City, CriteriaCity> GetDataGet(ISdk sdk) => sdk.Cities;
    }
}