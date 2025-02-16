using System.Security.Claims;
using Deve.Auth;
using Deve.Auth.Converters;
using Deve.Auth.TokenManagers;
using Deve.Api.DataSourceBuilder;

namespace Deve.Api.Controllers
{
    public class ControllerBaseAuth : ControllerBaseDeve
    {
        #region Constructor
        public ControllerBaseAuth(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder, ITokenManager tokenManager)
            : base(contextAccessor, dataSourceBuilder, tokenManager)
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