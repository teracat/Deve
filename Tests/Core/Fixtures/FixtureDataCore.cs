using Deve.Authenticate;
using Deve.Auth.TokenManagers.Jwt;
using Deve.Core;

namespace Deve.Tests.Core.Fixtures
{
    public class FixtureDataCore : IFixtureData<ICore>
    {
        public ICore DataNoAuth { get; private set; }
        public ICore DataValidAuth { get; private set; }

        public FixtureDataCore()
        {
            //IsSharedInstance is set to true so the Login stores the User authenticated to avoid permissions errors
            DataNoAuth = new CoreMain(true, TestsHelpers.CreateDataSourceMock(), null, new TokenManagerJwt());

            DataValidAuth = new CoreMain(true, TestsHelpers.CreateDataSourceMock(), null, new TokenManagerJwt());
            DataValidAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid)).Wait();
        }
    }
}