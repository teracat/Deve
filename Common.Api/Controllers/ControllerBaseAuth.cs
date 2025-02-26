using System.Security.Claims;
using Deve.Auth;
using Deve.Auth.Converters;
using Deve.Core;

namespace Deve.Api.Controllers
{
    public class ControllerBaseAuth : ControllerBaseDeve
    {
        #region Constructor
        public ControllerBaseAuth(IHttpContextAccessor contextAccessor, ICore core)
            : base(core)
        {
            Core.UserIdentity = CreateUserIdentity(contextAccessor?.HttpContext?.User);
        }
        #endregion

        #region Methods
        private UserIdentity? CreateUserIdentity(ClaimsPrincipal? user)
        {
            if (user is null || user.Identity is null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            return UserConverter.ToUserIdentity(user);
        }
        #endregion
    }
}