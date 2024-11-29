using Deve.Internal.Sdk;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkClient : TestClient<ISdk>, IClassFixture<FixtureDataSdk>, IClassFixture<FixtureDataSdkLogged>
    {
        public TestInternalSdkClient(FixtureDataSdk fixtureData, FixtureDataSdkLogged fixtureDataLogged)
            : base(fixtureData, fixtureDataLogged)
        {
        }
    }
}