using Deve.Core;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBase
    {
        public const string ValidUsername = "teracat";
        public const string ValidPassword = "teracat";

        protected ICore CreateCore()
        {
            //Uncomment the following lines and set the ConnectionString for your DataSource, if needed
            //It should use an instance for Testing purposes only
            /*var config = new Deve.DataSource.DataSourceConfig()
            {
                ConnectionString = ""
            };
            Deve.DataSource.DataSourceFactory.SetConfig(config);*/

            //IsSharedInstance is set to true so the Login stores the User authenticated to avoid permissions errors
            return new CoreMain(true);
        }

        protected async Task<ICore> CreateCoreAndExecuteValidLogin()
        {
            ICore core = CreateCore();
            await core.Authenticate.Login(new UserCredentials(ValidUsername, ValidPassword));
            return core;
        }
    }
}