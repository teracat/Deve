namespace Deve.Auth.UserIdentityService
{
    /// <summary>
    /// Service to get the current user identity.
    /// </summary>
    public class DefaultUserIdentityService : IUserIdentityService
    {
        public IUserIdentity? UserIdentity { get; set; }

        public DefaultUserIdentityService()
        {
        }

        public DefaultUserIdentityService(IUserIdentity? userIdentity)
        {
            UserIdentity = userIdentity;
        }
    }
}
