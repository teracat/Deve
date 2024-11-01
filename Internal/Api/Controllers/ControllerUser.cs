using Microsoft.AspNetCore.Mvc;
using Deve.Api;

namespace Deve.Internal.Api
{
    [ApiController]
    [Route(ApiConstants.ApiPathUser)]
    public class ControllerUser : ControllerBaseAll<User, User, CriteriaUser>
    {
        protected override IDataAll<User, User, CriteriaUser> DataAll => Core.Users;

        public ControllerUser(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }
    }
}
