using Microsoft.AspNetCore.Mvc;
using Deve.Api;
using Deve.DataSource;

namespace Deve.Internal.Api
{
    [ApiController]
    [Route(ApiConstants.PathUser)]
    public class ControllerUser : ControllerBaseAll<UserBase, UserPlainPassword, CriteriaUser>
    {
        protected override IDataAll<UserBase, UserPlainPassword, CriteriaUser> DataAll => Core.Users;

        public ControllerUser(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder)
            : base(contextAccessor, dataSourceBuilder)
        {
        }
    }
}
