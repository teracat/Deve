using Moq;
using Deve.Authenticate;
using Deve.Core;
using Deve.Cache;
using Deve.Internal.Data;
using Deve.Clients.Maui.Interfaces;
using Deve.Auth.UserIdentityService;

namespace Deve.Tests.Maui.Fixtures
{
    public class FixtureMaui : FixtureCommon
    {
        public IData DataNoAuth { get; }
        public IData DataValidAuth { get; }

        public Mock<INavigationService> NavigationService { get; }
        private ICache Cache { get; }
        private IUserIdentityService UserIdentityServiceNoAuth { get; }
        private IUserIdentityService UserIdentityServiceAuth { get; }

        public FixtureMaui()
        {
            Cache = new SimpleInMemoryCache();
            UserIdentityServiceNoAuth = new EmbeddedUserIdentityService();
            UserIdentityServiceAuth = new EmbeddedUserIdentityService();
            DataNoAuth = new CoreMain(DataSource, Auth, Options, UserIdentityServiceNoAuth, Cache);
            DataValidAuth = new CoreMain(DataSource, Auth, Options, UserIdentityServiceAuth, Cache);

            NavigationService = new Mock<INavigationService>();
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
            return Task.CompletedTask;
        }
    }
}
