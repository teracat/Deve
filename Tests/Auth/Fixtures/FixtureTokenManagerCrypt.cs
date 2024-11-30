using Deve.Auth;

namespace Deve.Tests.Auth
{
    public class FixtureTokenManagerCrypt : IFixtureTokenManager
    {
        public ITokenManager TokenManager { get; private set; }
        public string TokenExpired => "P83hovvDJI9+6LMyV9Tv/BtOITQB7fVvmMl2jvomZW5IpkmGq36VHUlx52Pygkn1+PiypZ6VinBVRdh9BPPX0lLg4kA6vIJjlxRN1h/EF+kx2PSjqlXSfnI0Bu2G1IFqyzQacf2JEPAyaCQuw+M/xQ==";

        public FixtureTokenManagerCrypt()
        {
            TokenManager = new TokenManagerCrypt(new CryptAes());
        }
    }
}
