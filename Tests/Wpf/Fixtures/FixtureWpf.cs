using Moq;
using Deve.Authenticate;
using Deve.Core;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Tests.Wpf.Fixtures
{
    public class FixtureWpf : IAsyncLifetime
    {
        public Mock<INavigationService> NavigationService { get; private set; }

        public IDataService DataServiceNoAuth { get; private set; }
        
        public IDataService DataServiceValidAuth { get; private set; }

        public Mock<IMessageHandler> MessageHandler { get; private set; }

        public FixtureWpf()
        {
            NavigationService = new Mock<INavigationService>();
            MessageHandler = new Mock<IMessageHandler>();

            // IsSharedInstance is set to false to get a new instance of the Data object
            var dataNoAuth = CoreFactory.Get(false, TestsHelpers.CreateDataSourceMock(), null);

            // IsSharedInstance is set to true so the Login stores the User authenticated to avoid permissions errors
            var dataValidAuth = CoreFactory.Get(true, TestsHelpers.CreateDataSourceMock(), null);
            
            DataServiceNoAuth = new FixtureDataService(dataNoAuth);
            DataServiceValidAuth = new FixtureDataService(dataValidAuth);
        }

        #region IAsyncLifetime
        public virtual async Task InitializeAsync()
        {
            await DataServiceValidAuth.Data.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));
        }

        public virtual async Task DisposeAsync()
        {
            await Task.Run(() =>
            {
                DataServiceNoAuth.Dispose();
                DataServiceValidAuth.Dispose();
            });
        }
        #endregion
    }
}