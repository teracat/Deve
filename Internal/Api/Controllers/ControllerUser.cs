using Microsoft.AspNetCore.Mvc;
using Deve.Auth.TokenManagers;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Deve.Internal.Criteria;
using Deve.Api.Controllers;
using Deve.Api.DataSourceBuilder;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathUser)]
    public class ControllerUser : ControllerBaseAll<UserBase, UserPlainPassword, CriteriaUser>
    {
        protected override IDataAll<UserBase, UserPlainPassword, CriteriaUser> DataAll => Core.Users;

        public ControllerUser(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder, ITokenManager tokenManager)
            : base(contextAccessor, dataSourceBuilder, tokenManager)
        {
        }
    }
}