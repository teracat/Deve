using Deve.Authenticate;
using Deve.Data;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Sdk.Fixtures
{
    public abstract class FixtureSdk<TEntryPoint, TDataType> : FixtureApi<TEntryPoint>, IFixtureData<TDataType> where TEntryPoint : class where TDataType : IDataCommon
    {
        public TDataType DataNoAuth { get; private set; }
        public TDataType DataValidAuth { get; private set; }

        public FixtureSdk()
        {
            DataNoAuth = CreateData();

            DataValidAuth = CreateData();
            DataValidAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid)).Wait();
        }

        protected abstract TDataType CreateData();
    }
}