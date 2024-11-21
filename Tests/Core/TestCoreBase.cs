using Deve.Core;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBase
    {
        public const string ValidUsername = "teracat";
        public const string ValidPassword = "teracat";

        protected ICore CreateCore()
        {
            //IsSharedInstance is set to true so the Login stores the User authenticated to avoid permissions errors
            return new CoreMain(true, TestsHelpers.CreateDataSource());
        }

        protected async Task<ICore> CreateCoreAndExecuteValidLogin()
        {
            ICore core = CreateCore();
            await core.Authenticate.Login(new UserCredentials(ValidUsername, ValidPassword));
            return core;
        }
    }
}