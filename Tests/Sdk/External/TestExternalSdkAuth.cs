using Deve.External.Sdk;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkAuth : TestAuthenticate<ISdk>, IClassFixture<FixtureDataSdk>, IClassFixture<FixtureDataSdkLogged>
    {
        public TestExternalSdkAuth(FixtureDataSdk fixtureData, FixtureDataSdkLogged fixtureDataLogged)
            : base(fixtureData, fixtureDataLogged)
        {
        }
    }
}