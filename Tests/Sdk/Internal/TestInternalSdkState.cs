using Deve.Criteria;
using Deve.Internal.Data;
using Deve.Internal.Sdk;
using Deve.Model;
using Deve.Tests.Sdk.Internal.Fixtures;


namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkState : TestState<ISdk>, IClassFixture<FixtureSdkInternal>
    {
        public TestInternalSdkState(FixtureSdkInternal fixture)
            : base(fixture)
        {
        }

        protected override IDataAll<State, State, CriteriaState> GetDataAll(ISdk sdk) => sdk.States;
    }
}