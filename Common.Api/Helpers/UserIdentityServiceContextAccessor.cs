using Deve.Auth;
using Deve.Auth.UserIdentityService;

namespace Deve.Api.Helpers
{
    /// <summary>
    /// User requesting the data, retrieved using the IHttpContextAccessor.
    /// </summary>
    public class UserIdentityServiceContextAccessor : IUserIdentityService
    {
        /// <summary>
        /// User identity.
        /// </summary>
        public IUserIdentity? UserIdentity { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="contextAccessor">HttpContext accessor.</param>
        public UserIdentityServiceContextAccessor(IHttpContextAccessor contextAccessor)
        {
            UserIdentity = new UserIdentityFromContextAccessor(contextAccessor);
        }
    }
}
