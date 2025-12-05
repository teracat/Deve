using Moq;
using Deve.Auth.UserIdentityService;
using Deve.Authenticate;
using Deve.Cache;
using Deve.Clients.Wpf.Interfaces;
using Deve.Core;
using Deve.Internal.Data;

namespace Deve.Tests.Wpf.Fixtures
{
    public class FixtureWpf : FixtureCommon
    {
        #region Properties
        public IData DataNoAuth { get; }
        public IData DataValidAuth { get; }

        public Mock<IMessageHandler> MessageHandler { get; }
        public Mock<INavigationService> NavigationService { get; }
        private ICache Cache { get; }
        private IUserIdentityService UserIdentityServiceNoAuth { get; }
        private IUserIdentityService UserIdentityServiceAuth { get; }
        #endregion

        #region Constructor
        public FixtureWpf()
        {
            Cache = new SimpleInMemoryCache();
            UserIdentityServiceNoAuth = new EmbeddedUserIdentityService();
            UserIdentityServiceAuth = new EmbeddedUserIdentityService();
            DataNoAuth = new CoreMain(DataSource, Auth, Options, UserIdentityServiceNoAuth, Cache);
            DataValidAuth = new CoreMain(DataSource, Auth, Options, UserIdentityServiceAuth, Cache);

            NavigationService = new Mock<INavigationService>();
            MessageHandler = new Mock<IMessageHandler>();
        }
        #endregion

        #region IAsyncLifetime
        public override async Task InitializeAsync() => await DataValidAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));

        public override Task DisposeAsync()
        {
            DataNoAuth.Dispose();
            DataValidAuth.Dispose();
            Cache.Dispose();
            return base.DisposeAsync();
        }
        #endregion
    }
}
