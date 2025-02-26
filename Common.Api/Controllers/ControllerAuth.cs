using Microsoft.AspNetCore.Mvc;
using Deve.Authenticate;
using Deve.Model;
using Deve.Core;

namespace Deve.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathAuth)]
    public class ControllerAuth : ControllerBaseDeve
    {
        public ControllerAuth(ICore core)
            : base(core)
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