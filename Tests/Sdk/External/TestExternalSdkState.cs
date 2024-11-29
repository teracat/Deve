using Deve.External;
using Deve.External.Sdk;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkState : TestBaseDataGet<ISdk, State, State, CriteriaState>, IClassFixture<FixtureDataSdk>, IClassFixture<FixtureDataSdkLogged>
    {
        public TestExternalSdkState(FixtureDataSdk fixtureData, FixtureDataSdkLogged fixtureDataLogged)
            : base(fixtureData, fixtureDataLogged)
        {
        }

        protected override IDataGet<State, State, CriteriaState> GetDataGet(ISdk sdk) => sdk.States;
    }
}