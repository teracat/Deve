using Deve.Api.Controllers;
using Deve.Internal.Criteria;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Microsoft.AspNetCore.Mvc;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathUser)]
    public class ControllerUser : ControllerBaseAll<UserBase, UserPlainPassword, CriteriaUser>
    {
        private readonly IDataUser _data;

        protected override IDataAll<UserBase, UserPlainPassword, CriteriaUser> DataAll => _data;

        public ControllerUser(IDataUser data)
        {
            _data = data;
        }
    }
}
