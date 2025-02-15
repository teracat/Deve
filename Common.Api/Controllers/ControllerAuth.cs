using Microsoft.AspNetCore.Mvc;
using Deve.Authenticate;
using Deve.Model;
using Deve.Auth.TokenManagers;
using Deve.Api.DataSourceBuilder;

namespace Deve.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathAuth)]
    public class ControllerAuth : ControllerBaseDeve
    {
        public ControllerAuth(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder, ITokenManager tokenManager)
            : base(contextAccessor, dataSourceBuilder, tokenManager)
        {
        }

        [HttpGet(ApiConstants.MethodLogin)]
        public Task<ResultGet<UserToken>> Login([FromQuery]UserCredentials credentials)
        {
            return Core.Authenticate.Login(credentials);
        }

        [HttpGet(ApiConstants.MethodRefreshToken)]
        public Task<ResultGet<UserToken>> RefreshToken([FromQuery] string token)
        {
            return Core.Authenticate.RefreshToken(token);
        }
    }
}