using Deve.External.Sdk;

namespace Deve.Internal.Sdk
{
    internal class SdkStateAll : SdkBaseAll<State, State, CriteriaState>
    {
        #region Properties
        protected override string Path => ApiConstants.ApiPathState;
        #endregion

        #region Constructor
        public SdkStateAll(ISdk sdk)
            : base(sdk)
        {
        }
        #endregion
    }
}
