using Deve.Internal;
using Deve.Internal.Sdk;

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