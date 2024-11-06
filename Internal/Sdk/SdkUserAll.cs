namespace Deve.Internal.Sdk
{
    internal class SdkUserAll : SdkBaseAll<User, User, CriteriaUser>
    {
        #region Properties
        protected override string Path => ApiConstants.ApiPathUser;
        #endregion

        #region Constructor
        public SdkUserAll(ISdk sdk)
            : base(sdk)
        {
        }
        #endregion
    }
}
