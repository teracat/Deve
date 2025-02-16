using Deve.Authenticate;
using Deve.Model;
using Deve.Auth;
using Deve.Auth.Crypt;
using Deve.Auth.TokenManagers;

namespace Deve.Tests.Auth.Fixtures
{
    public class FixtureAuth : IDisposable
    {
        private readonly ITokenManager _tokenManager;

        public IAuth Auth { get; private set; }
        public ICrypt Crypt { get; private set; }
        public UserToken UserTokenValid { get; private set; }
        public UserToken UserTokenInactive { get; private set; }
        public UserIdentity UserIdentityUser { get; private set; }
        public UserIdentity UserIdentityAdmin { get; private set; }

        public FixtureAuth()
        {
            _tokenManager = TestsHelpers.CreateTokenManager();
            UserTokenValid = TestsHelpers.CreateTokenValid(_tokenManager);
            UserTokenInactive = TestsHelpers.CreateTokenInactiveUser(_tokenManager);
            Crypt = new CryptAes(TestsConstants.CryptAesKey, TestsConstants.CryptAesIV);
            Auth = TestsHelpers.CreateAuth(_tokenManager);   //Must be after CreateToken so the TokenManager is set to JWT
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

        public void Dispose()
        {
            Auth.Dispose();
            Crypt.Dispose();
            _tokenManager.Dispose();
        }
    }
}