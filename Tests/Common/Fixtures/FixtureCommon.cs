using Deve.Auth;
using Deve.Auth.Hash;
using Deve.Auth.Crypt;
using Deve.Auth.TokenManagers;
using Deve.Data;
using Deve.DataSource;

namespace Deve.Tests
{
    public class FixtureCommon : IAsyncLifetime
    {
        #region Properties
        public IDataOptions Options { get; }
        public IHash Hash { get; }
        public ICrypt Crypt { get; private set; }
        public ITokenManager TokenManager { get; }
        public IDataSource DataSource { get; }
        public IAuth Auth { get; }
        #endregion

        #region Constructor
        public FixtureCommon()
        {
            Options = new DataOptions();
            Hash = TestsHelpers.CreateHash();
            Crypt = TestsHelpers.CreateCrypt();
            TokenManager = TestsHelpers.CreateTokenManager();
            DataSource = TestsHelpers.CreateDataSourceMock();
            Auth = TestsHelpers.CreateAuth(TokenManager, DataSource, Hash, Options);
        }
        #endregion

        #region IAsyncLifetime
        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task DisposeAsync()
        {
            Auth.Dispose();
            DataSource.Dispose();
            TokenManager.Dispose();
            Crypt.Dispose();
            Hash.Dispose();
            return Task.CompletedTask;
        }
        #endregion
    }
}