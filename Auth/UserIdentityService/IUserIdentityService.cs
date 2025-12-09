namespace Deve.Auth.UserIdentityService
{
    /// <summary>
    /// Service to get the current user identity.
    /// </summary>
    public interface IUserIdentityService
    {
        IUserIdentity? UserIdentity { get; set; }
    }
}
