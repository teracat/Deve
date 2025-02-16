using Deve.Auth.TokenManagers;
using Deve.Authenticate;
using Deve.Core;
using Deve.Clients.Maui.Interfaces;
using Deve.Tests.Maui.Mocks;

namespace Deve.Tests.Maui.Fixtures
{
    public class FixtureMaui : IAsyncLifetime
    {
        private readonly ITokenManager _tokenManager;
        public MockNavigationService NavigationService { get; private set; }
        public IDataService DataServiceNoAuth { get; private set; }
        public IDataService DataServiceValidAuth { get; private set; }

        public FixtureMaui()
        {
            _tokenManager = TestsHelpers.CreateTokenManager();
            NavigationService = new MockNavigationService();

            // IsSharedInstance is set to false to get a new instance of the Data object
            var dataNoAuth = CoreFactory.Get(false, _tokenManager, TestsHelpers.CreateDataSourceMock());

            // IsSharedInstance is set to true so the Login stores the User authenticated to avoid permissions errors
            var dataValidAuth = CoreFactory.Get(true, _tokenManager, TestsHelpers.CreateDataSourceMock());
            
            DataServiceNoAuth = new FixtureDataService(dataNoAuth);
            DataServiceValidAuth = new FixtureDataService(dataValidAuth);
        }

        public async Task InitializeAsync()
        {
            await DataServiceValidAuth.Data.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));
        }

        public Task DisposeAsync()
        {
            DataServiceNoAuth.Dispose();
            DataServiceValidAuth.Dispose();
            _tokenManager.Dispose();
            return Task.CompletedTask;
        }
    }
}