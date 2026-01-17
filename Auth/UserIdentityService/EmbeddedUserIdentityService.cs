using Deve.Identity.Enums;

namespace Deve.Auth.UserIdentityService;

/// <summary>
/// Service to get the current user identity.
/// Used for embedded application (see WPF client sample).
/// </summary>
public class EmbeddedUserIdentityService : IUserIdentityService
{
    public UserIdentity? UserIdentity { get; set; }

    public bool IsAuthenticated => UserIdentity is not null;

    public bool IsAdmin => UserIdentity is not null && UserIdentity.Role == Role.Admin;

    public EmbeddedUserIdentityService()
    {
    }

    public EmbeddedUserIdentityService(UserIdentity? userIdentity)
    {
        UserIdentity = userIdentity;
    }
}
