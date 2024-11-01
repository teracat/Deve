namespace Deve.External.Sdk
{
    internal class SdkStateGet : SdkBaseGet<State, State, CriteriaState, ISdk>
    {
        #region Properties
        protected override string Path => ApiConstants.ApiPathState;
        #endregion

        #region Constructor
        public SdkStateGet(ISdk sdk)
            : base(sdk)
        {
        }
        #endregion
    }
}
