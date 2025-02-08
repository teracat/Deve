using Deve.ClientApp.Maui.Interfaces;
using Deve.Core;

namespace Deve.Tests.Maui.Fixtures
{
    public class FixtureMaui
    {
        public FixtureNavigationService NavigationService { get; private set; }
        public IDataService DataServiceNoAuth { get; private set; }
        public IDataService DataServiceValidAuth { get; private set; }

        public FixtureMaui()
        {
            NavigationService = new FixtureNavigationService();

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