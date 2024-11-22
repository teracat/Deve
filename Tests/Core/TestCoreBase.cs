using Deve.Core;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBase
    {
        protected ICore CreateCore()
        {
            //IsSharedInstance is set to true so the Login stores the User authenticated to avoid permissions errors
            return new CoreMain(true, TestsHelpers.CreateDataSourceMock());
        }

        protected async Task<ICore> CreateCoreAndExecuteValidLogin()
        {
            ICore core = CreateCore();
            await core.Authenticate.Login(new UserCredentials(TestsHelpers.UserUsernameValid, TestsHelpers.UserPasswordValid));
            return core;
        }
    }
}