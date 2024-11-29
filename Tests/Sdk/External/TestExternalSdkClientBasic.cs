using Deve.External;
using Deve.External.Sdk;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkClientBasic : TestBaseDataGet<ISdk, ClientBasic, Client, CriteriaClientBasic>, IClassFixture<FixtureDataSdk>, IClassFixture<FixtureDataSdkLogged>
    {
        public TestExternalSdkClientBasic(FixtureDataSdk fixtureData, FixtureDataSdkLogged fixtureDataLogged)
            : base(fixtureData, fixtureDataLogged)
        {
        }

        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> GetDataGet(ISdk sdk) => sdk.Clients;
    }
}