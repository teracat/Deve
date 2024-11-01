using System.Security.Claims;
using Deve.Auth;

namespace Deve.Api
{
    public class ControllerBaseAuth : ControllerBaseDeve
    {
        #region Constructor
        public ControllerBaseAuth(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
            Core.UserIdentity = CreateUserIdentity(contextAccessor?.HttpContext?.User);
        }
        #endregion

        #region Methods
        private UserIdentity? CreateUserIdentity(ClaimsPrincipal? user)
        {
            if (user is null || user.Identity is null || !user.Identity.IsAuthenticated)
                return null;

            return UserConverter.ToUserIdentity(user);
        }
        #endregion
    }
}
