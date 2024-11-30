using Deve.External;
using Deve.External.Sdk;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkState : TestBaseDataGet<ISdk, State, State, CriteriaState>, IClassFixture<FixtureSdkExternal>
    {
        public TestExternalSdkState(FixtureSdkExternal fixture)
            : base(fixture)
        {
        }

        protected override IDataGet<State, State, CriteriaState> GetDataGet(ISdk sdk) => sdk.States;
    }
}