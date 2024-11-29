using Deve.External.Sdk;

namespace Deve.Tests.Sdk.External
{
    public class FixtureDataSdkLogged : FixtureDataSdk, IFixtureDataLogged<ISdk>
    {
        public FixtureDataSdkLogged()
        {
            Data.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid)).Wait();
        }
    }
}
