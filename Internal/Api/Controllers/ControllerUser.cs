using Microsoft.AspNetCore.Mvc;
using Deve.Core;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Deve.Internal.Criteria;
using Deve.Api.Controllers;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathUser)]
    public class ControllerUser : ControllerBaseAll<UserBase, UserPlainPassword, CriteriaUser>
    {
        protected override IDataAll<UserBase, UserPlainPassword, CriteriaUser> DataAll => Core.Users;

        public ControllerUser(IHttpContextAccessor contextAccessor, ICore core)
            : base(contextAccessor, core)
        {
        }
    }
}