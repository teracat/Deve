namespace Deve.Internal.Sdk
{
    internal class SdkUserAll : SdkBaseAll<UserBase, UserPlainPassword, CriteriaUser>
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
