using Deve.Core;

namespace Deve.Tests.Core
{
    public class FixtureDataCoreLogged : FixtureDataCore, IFixtureDataLogged<ICore>
    {
        public FixtureDataCoreLogged()
        {
            Data.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid)).Wait();
        }
    }
}
