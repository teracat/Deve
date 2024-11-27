namespace Deve.Tests
{
    public abstract class TestBase<TDataType> where TDataType : IDataCommon
    {
        protected abstract TDataType CreateData();

        protected async Task<TDataType> CreateDataAndExecuteValidLogin()
        {
            var data = CreateData();
            await data.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));
            return data;
        }
    }
}