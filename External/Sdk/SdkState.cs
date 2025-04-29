using Deve.Model;
using Deve.Criteria;
using Deve.External.Data;

namespace Deve.External.Sdk
{
    internal class SdkState : SdkBaseGet<State, State, CriteriaState, ISdk>, IDataState
    {
        public SdkState(ISdk sdk)
            : base(ApiConstants.PathState, sdk)
        {
        }
    }
}
