using Deve.Dto;
using Deve.Internal.Data;

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
