using Deve.Auth.Crypt;
using Deve.Auth.TokenManagers;

namespace Deve.Tests.Auth.Fixtures
{
    public class FixtureTokenManagerCrypt : IFixtureTokenManager
    {
        public ITokenManager TokenManager { get; private set; }
        public string TokenExpired => "XlaXyKNhgfNLFgpODxlfo/t6d92bvlhrExOpXDGsLGQtCt559Gd82kF7NST+hw1PtuFQt4EEa2fwAo6HrZ81LNt5WNjndvAK3F+84ZrDZ4dEvVWhB0B4huiiVfKTmB5R4mH+jThsKprNds1W6movzw==";

        public FixtureTokenManagerCrypt()
        {
            TokenManager = new TokenManagerCrypt(new CryptAes(TestsConstants.CryptAesKey, TestsConstants.CryptAesIV), true);
        }
    }
}