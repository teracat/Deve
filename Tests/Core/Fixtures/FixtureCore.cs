using Deve.Auth.UserIdentityService;
using Deve.Authenticate;
using Deve.Cache;
using Deve.Core;

namespace Deve.Tests.Core.Fixtures
{
    public class FixtureCore : FixtureCommon, IFixtureData<ICore>
    {
        public ICore DataNoAuth { get; }
        public ICore DataValidAuth { get; }

        private ICache Cache { get; }
        private IUserIdentityService UserIdentityServiceNoAuth { get; }
        private IUserIdentityService UserIdentityServiceAuth { get; }

        public FixtureCore()
        {
            Cache = new SimpleInMemoryCache();
            UserIdentityServiceNoAuth = new EmbeddedUserIdentityService();
            UserIdentityServiceAuth = new EmbeddedUserIdentityService();
            DataNoAuth = new CoreMain(DataSource, Auth, Options, UserIdentityServiceNoAuth, Cache);
            DataValidAuth = new CoreMain(DataSource, Auth, Options, UserIdentityServiceAuth, Cache);
        }

        public override async Task InitializeAsync()
        {
            await DataValidAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));
        }

        public override Task DisposeAsync()
        {
            DataNoAuth.Dispose();
            DataValidAuth.Dispose();
            Cache.Dispose();
            return base.DisposeAsync();
        }
    }
}
