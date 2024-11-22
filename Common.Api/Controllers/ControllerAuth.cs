using Deve.DataSource;
using Microsoft.AspNetCore.Mvc;

namespace Deve.Api
{
    [ApiController]
    [Route(ApiConstants.PathAuth)]
    public class ControllerAuth : ControllerBaseDeve
    {
        public ControllerAuth(IHttpContextAccessor contextAccessor, IDataSourceFactory dataSourceFactory)
            : base(contextAccessor, dataSourceFactory)
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
