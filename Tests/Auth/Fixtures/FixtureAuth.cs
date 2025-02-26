using Deve.Model;
using Deve.Authenticate;
using Deve.Auth;

namespace Deve.Tests.Auth.Fixtures
{
    public class FixtureAuth : FixtureCommon
    {
        public UserToken UserTokenValid { get; private set; }
        public UserToken UserTokenInactive { get; private set; }

        public UserIdentity UserIdentityUser { get; private set; }
        public UserIdentity UserIdentityAdmin { get; private set; }

        public FixtureAuth()
        {
            UserTokenValid = TestsHelpers.CreateTokenValid(TokenManager);
            UserTokenInactive = TestsHelpers.CreateTokenInactiveUser(TokenManager);

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