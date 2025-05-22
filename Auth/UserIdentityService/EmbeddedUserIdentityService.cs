namespace Deve.Auth.UserIdentityService
{
    /// <summary>
    /// Service to get the current user identity.
    /// Used for embedded application (see WPF client sample).
    /// </summary>
    public class EmbeddedUserIdentityService : IUserIdentityService
    {
        public IUserIdentity? UserIdentity { get; set; }

        public EmbeddedUserIdentityService()
        {
        }

        public EmbeddedUserIdentityService(IUserIdentity? userIdentity)
        {
            UserIdentity = userIdentity;
        }
    }
}
