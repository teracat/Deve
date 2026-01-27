using Deve.Auth.TokenManagers;
using Deve.Crypt;

namespace Deve.Tests.Auth.Fixtures;

public class TokenManagerCryptFixture : ITokenManagerFixture, IDisposable
{
    public ICrypt Crypt { get; }
    public ITokenManager TokenManager { get; }
    public string TokenExpired => "XlaXyKNhgfNLFgpODxlfo/t6d92bvlhrExOpXDGsLGQtCt559Gd82kF7NST+hw1PtuFQt4EEa2fwAo6HrZ81LNt5WNjndvAK3F+84ZrDZ4dEvVWhB0B4huiiVfKTmB5R4mH+jThsKprNds1W6movzw==";

    private bool _disposed;

    public TokenManagerCryptFixture()
    {
        Crypt = new CryptAes(TestsConstants.CryptAesKey, TestsConstants.CryptAesIV);
        TokenManager = new TokenManagerCrypt(Crypt, true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Crypt.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
