using Deve.Authenticate;
using Deve.Data;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Sdk.Fixtures
{
    public abstract class FixtureSdk<TEntryPoint, TDataType> : FixtureApi<TEntryPoint>, IFixtureData<TDataType>, IAsyncLifetime where TEntryPoint : class where TDataType : IDataCommon
    {
        public TDataType DataNoAuth { get; private set; }
        public TDataType DataValidAuth { get; private set; }

        protected FixtureSdk()
        {
            DataNoAuth = CreateData();
            DataValidAuth = CreateData();
        }

        protected abstract TDataType CreateData();

        protected override void Dispose(bool disposing)
        {
            DataNoAuth.Dispose();
            DataValidAuth.Dispose();
            base.Dispose(disposing);
        }

        public async Task InitializeAsync()
        {
            await DataValidAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));
        }

        Task IAsyncLifetime.DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}