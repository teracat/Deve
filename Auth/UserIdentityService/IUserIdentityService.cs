namespace Deve.Auth.UserIdentityService;

/// <summary>
/// Service to get the current user identity.
/// </summary>
public interface IUserIdentityService
{
    UserIdentity? UserIdentity { get; set; }

    bool IsAuthenticated { get; }
    bool IsAdmin { get; }
}
