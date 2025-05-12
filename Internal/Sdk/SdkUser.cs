using Deve.Internal.Data;
using Deve.Internal.Criteria;
using Deve.Internal.Model;

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
