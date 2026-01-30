using Deve.Auth;
using Deve.Identity.Enums;

namespace Deve.Tests.Auth.Fixtures;

public class AuthFixture : CommonFixture
{
    public UserToken UserTokenValid { get; }
    public UserToken AdminTokenValid { get; }
    public UserToken UserTokenInactive { get; }

    public UserIdentity UserIdentityUser { get; }
    public UserIdentity UserIdentityAdmin { get; }

    public AuthFixture()
    {
        UserTokenValid = TestsHelpers.CreateTokenValid(TokenManager, Role.User);
        AdminTokenValid = TestsHelpers.CreateTokenValid(TokenManager, Role.Admin);
        UserTokenInactive = TestsHelpers.CreateTokenInactiveUser(TokenManager, Role.User);

        UserIdentityUser = new UserIdentity(Guid.NewGuid(), "user.tests", Role.User);
        UserIdentityAdmin = new UserIdentity(Guid.NewGuid(), "admin.tests", Role.Admin);
    }
}
