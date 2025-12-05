using Deve.Criteria;
using Deve.External.Data;
using Deve.Model;

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
