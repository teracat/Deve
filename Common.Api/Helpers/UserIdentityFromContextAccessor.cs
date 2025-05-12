using Deve.Auth;
using Deve.Auth.Converters;

namespace Deve.Api.Helpers
{
    /// <summary>
    /// User requesting the data, retrieved using the IHttpContextAccessor.
    /// </summary>
    public class UserIdentityFromContextAccessor : UserIdentity
    {
        public UserIdentityFromContextAccessor(IHttpContextAccessor contextAccessor)
        {
            var claims = contextAccessor?.HttpContext?.User;
            if (claims?.Identity?.IsAuthenticated ?? false)
            {
                UserConverter.ToUserIdentity(claims, this);
            }
        }
    }
}
