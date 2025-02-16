using Moq;
using Deve.Authenticate;
using Deve.Core;
using Deve.Clients.Wpf.Interfaces;
using Deve.DataSource;
using Deve.Auth.TokenManagers;

namespace Deve.Tests.Wpf.Fixtures
{
    public class FixtureWpf : IAsyncLifetime
    {
        #region Fields
        private readonly ITokenManager _tokenManager = TestsHelpers.CreateTokenManager();
        private readonly IDataSource _dataSource = TestsHelpers.CreateDataSourceMock();
        #endregion

        #region Properties
        public Mock<INavigationService> NavigationService { get; private set; }

        public IDataService DataServiceNoAuth { get; private set; }
        
        public IDataService DataServiceValidAuth { get; private set; }

        public Mock<IMessageHandler> MessageHandler { get; private set; }
        #endregion

        #region Constructor
        public FixtureWpf()
        {
            NavigationService = new Mock<INavigationService>();
            MessageHandler = new Mock<IMessageHandler>();

            // IsSharedInstance is set to false to get a new instance of the Data object
            var dataNoAuth = CoreFactory.Get(false, _tokenManager, _dataSource);

            // IsSharedInstance is set to true so the Login stores the User authenticated to avoid permissions errors
            var dataValidAuth = CoreFactory.Get(true, _tokenManager, _dataSource);
            
            DataServiceNoAuth = new FixtureDataService(dataNoAuth);
            DataServiceValidAuth = new FixtureDataService(dataValidAuth);
        }
        #endregion

        #region IAsyncLifetime
        public virtual async Task InitializeAsync()
        {
            await DataServiceValidAuth.Data.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));
        }

        public virtual Task DisposeAsync()
        {
            DataServiceNoAuth.Dispose();
            DataServiceValidAuth.Dispose();
            _tokenManager.Dispose();
            return Task.CompletedTask;
        }
        #endregion
    }
}