using Deve.Auth;
using Deve.Auth.Converters;
using Deve.Auth.UserIdentityService;

namespace Deve.Api.Auth
{
    /// <summary>
    /// UserIdentity requesting the data, retrieved using the IHttpContextAccessor.
    /// </summary>
    public class ContextAccessorUserIdentityService : IUserIdentityService
    {
        /// <summary>
        /// User identity.
        /// </summary>
        public IUserIdentity? UserIdentity { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        public ContextAccessorUserIdentityService(IHttpContextAccessor contextAccessor)
        {
            var claims = contextAccessor?.HttpContext?.User;
            if (claims?.Identity?.IsAuthenticated ?? false)
            {
                UserIdentity = UserConverter.ToUserIdentity(claims);
            }
        }
    }
}
