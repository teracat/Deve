using Microsoft.AspNetCore.Mvc;
using Deve.Api.Controllers;
using Deve.Internal.Data;
using Deve.Internal.Dto;

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
