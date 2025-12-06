using Deve.Authenticate;
using Deve.Model;
using Microsoft.AspNetCore.Mvc;

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
        public Task<ResultGet<UserToken>> Login([FromQuery] UserCredentials credentials) => _auth.Login(credentials);

        [HttpGet(ApiConstants.MethodRefreshToken)]
        public Task<ResultGet<UserToken>> RefreshToken([FromQuery] string token) => _auth.RefreshToken(token);
    }
}
