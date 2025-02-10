using Deve.Authenticate;
using Deve.Core;
using Deve.Clients.Wpf.Interfaces;
using Deve.Tests.Wpf.Mocks;

namespace Deve.Tests.Wpf.Fixtures
{
    public class FixtureWpf
    {
        public MockNavigationService NavigationService { get; private set; }
        public IDataService DataServiceNoAuth { get; private set; }
        public IDataService DataServiceValidAuth { get; private set; }

        public FixtureWpf()
        {
            NavigationService = new MockNavigationService();

            // IsSharedInstance is set to false to get a new instance of the Data object
            var dataNoAuth = CoreFactory.Get(false, TestsHelpers.CreateDataSourceMock(), null);

            // IsSharedInstance is set to true so the Login stores the User authenticated to avoid permissions errors
            var dataValidAuth = CoreFactory.Get(true, TestsHelpers.CreateDataSourceMock(), null);
            dataValidAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid)).Wait();

            DataServiceNoAuth = new FixtureDataService(dataNoAuth);
            DataServiceValidAuth = new FixtureDataService(dataValidAuth);
        }
    }
}