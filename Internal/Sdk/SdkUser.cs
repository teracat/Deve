using Deve.Internal.Dto;
using Deve.Internal.Data;

namespace Deve.Internal.Sdk
{
    internal class SdkUser : SdkBaseAll<UserBase, UserPlainPassword, CriteriaUser>, IDataUser
    {
        public SdkUser(ISdk sdk)
            : base(ApiConstants.PathUser, sdk)
        {
        }
    }
}
