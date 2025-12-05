using Deve.Criteria;
using Deve.Internal.Data;
using Deve.Model;

namespace Deve.Internal.Sdk
{
    internal class SdkState : SdkBaseAll<State, State, CriteriaState>, IDataState
    {
        public SdkState(ISdk sdk)
            : base(ApiConstants.PathState, sdk)
        {
        }
    }
}
