using Deve.Internal.Sdk;

namespace Deve.Tests.Sdk.Internal
{
    public class FixtureDataSdkLogged : FixtureDataSdk, IFixtureDataLogged<ISdk>
    {
        public FixtureDataSdkLogged()
        {
            Data.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid)).Wait();
        }
    }
}
