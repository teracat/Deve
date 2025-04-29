using Microsoft.AspNetCore.Mvc;
using Deve.Authenticate;
using Deve.Model;

namespace Deve.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathAuth)]
    public class ControllerAuth : ControllerBaseDeve
    {
        private readonly IAuthenticate _auth;

        public ControllerAuth(IAuthenticate auth)
        {
            _auth = auth;
        }

        [HttpGet(ApiConstants.MethodLogin)]
        public Task<ResultGet<UserToken>> Login([FromQuery]UserCredentials credentials)
        {
            return _auth.Login(credentials);
        }

        [HttpGet(ApiConstants.MethodRefreshToken)]
        public Task<ResultGet<UserToken>> RefreshToken([FromQuery] string token)
        {
            return _auth.RefreshToken(token);
        }
    }
}
