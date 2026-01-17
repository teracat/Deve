using Deve.Auth.TokenManagers;
using Deve.Crypt;
using Deve.Data;
using Deve.Hash;

namespace Deve.Tests;

public class CommonFixture : IAsyncLifetime
{
    #region Properties
    public IDataOptions Options { get; }
    public IHash Hash { get; }
    public ICrypt Crypt { get; private set; }
    public ITokenManager TokenManager { get; }
    #endregion

    #region Constructor
    public CommonFixture()
    {
        Options = new DataOptions();
        Hash = TestsHelpers.CreateHash();
        Crypt = TestsHelpers.CreateCrypt();
        TokenManager = TestsHelpers.CreateTokenManager();
    }
    #endregion

    #region IAsyncLifetime
    public virtual Task InitializeAsync() => Task.CompletedTask;

    public virtual Task DisposeAsync()
    {
        TokenManager.Dispose();
        Crypt.Dispose();
        Hash.Dispose();
        return Task.CompletedTask;
    }
    #endregion
}
