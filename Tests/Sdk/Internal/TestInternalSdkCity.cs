using Deve.Internal;
using Deve.Internal.Sdk;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkCity : TestCity<ISdk>, IClassFixture<FixtureDataSdk>, IClassFixture<FixtureDataSdkLogged>
    {
        public TestInternalSdkCity(FixtureDataSdk fixtureData, FixtureDataSdkLogged fixtureDataLogged)
            : base(fixtureData, fixtureDataLogged)
        {
        }

        protected override IDataAll<City, City, CriteriaCity> GetDataAll(ISdk sdk) => sdk.Cities;
    }
}