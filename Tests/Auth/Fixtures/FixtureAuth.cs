using Deve.Auth;

namespace Deve.Tests.Auth
{
    public class FixtureAuth
    {
        public IAuth Auth { get; private set; }
        public UserToken UserTokenValid { get; private set; }
        public UserToken UserTokenInactive { get; private set; }
        public UserIdentity UserIdentityUser { get; private set; }
        public UserIdentity UserIdentityAdmin { get; private set; }

        public FixtureAuth()
        {
            UserTokenValid = TestsHelpers.CreateTokenValid();
            UserTokenInactive = TestsHelpers.CreateTokenInactiveUser();
            Auth = TestsHelpers.CreateAuth();   //Must be after CreateToken so the TokenManager is set to JWT
            UserIdentityUser = new UserIdentity()
            {
                Id = 1,
                Role = Role.User,
            };
            UserIdentityAdmin = new UserIdentity()
            {
                Id = 1,
                Role = Role.Admin,
            };
        }
    }
}
