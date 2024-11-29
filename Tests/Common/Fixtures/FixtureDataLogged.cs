namespace Deve.Tests
{
    /// <summary>
    /// Will contain the IData implementation and it will also perform a Login when created
    /// </summary>
    /// <typeparam name="TDataType">DataType that will be used.</typeparam>
    public abstract class FixtureDataLogged<TDataType> : IFixtureDataLogged<TDataType> where TDataType : IDataCommon
    {
        public TDataType Data { get; }

        public FixtureDataLogged(TDataType data)
        {
            Data = data;
            Data.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));
        }
    }
}
