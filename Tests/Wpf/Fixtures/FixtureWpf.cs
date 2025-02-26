using Moq;
using Deve.Authenticate;
using Deve.Core;
using Deve.Internal.Data;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Tests.Wpf.Fixtures
{
    public class FixtureWpf : FixtureCommon
    {
        #region Properties
        public IData DataNoAuth { get; private set; }        
        public IData DataValidAuth { get; private set; }

        public Mock<IMessageHandler> MessageHandler { get; private set; }
        public Mock<INavigationService> NavigationService { get; private set; }
        #endregion

        #region Constructor
        public FixtureWpf()
        {
            DataNoAuth = new CoreMain(DataSource, Auth, Options);
            DataValidAuth = new CoreMain(DataSource, Auth, Options);

            NavigationService = new Mock<INavigationService>();
            MessageHandler = new Mock<IMessageHandler>();
        }
        #endregion

        #region IAsyncLifetime
        public override async Task InitializeAsync()
        {
            await DataValidAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));
        }

        public override Task DisposeAsync()
        {
            DataNoAuth.Dispose();
            DataValidAuth.Dispose();
            return base.DisposeAsync();
        }
        #endregion
    }
}