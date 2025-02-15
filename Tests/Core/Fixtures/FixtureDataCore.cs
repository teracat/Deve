using Deve.Authenticate;
using Deve.Auth.TokenManagers.Jwt;
using Deve.Core;
using Deve.DataSource;
using Deve.Auth.TokenManagers;

namespace Deve.Tests.Core.Fixtures
{
    public class FixtureDataCore : IFixtureData<ICore>, IAsyncLifetime
    {
        private readonly IDataSource _dataSource = TestsHelpers.CreateDataSourceMock();

        public ITokenManager TokenManager { get; } = new TokenManagerJwt(TestsConstants.JwtSigningSecretKey, TestsConstants.JwtEncryptionSecretKey);
        public ICore DataNoAuth { get; private set; }
        public ICore DataValidAuth { get; private set; }

        public FixtureDataCore()
        {
            //IsSharedInstance is set to true so the Login stores the User authenticated to avoid permissions errors
            DataNoAuth = new CoreMain(true, TokenManager, _dataSource);

            DataValidAuth = new CoreMain(true, TokenManager, _dataSource);
        }

        public async Task InitializeAsync()
        {
            await DataValidAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));
        }

        public Task DisposeAsync()
        {
            DataNoAuth.Dispose();
            DataValidAuth.Dispose();
            _dataSource.Dispose();
            TokenManager.Dispose();
            return Task.CompletedTask;
        }
    }
}