using Deve.Auth;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// TokenManagerCrypt Tests.
    /// </summary>
    public class TestTokenManagerCrypt : TestTokenManagerBase
    {
        protected override ITokenManager CreateTokenManager()
        {
            var crypt = new CryptAes();
            return new TokenManagerCrypt(ApiConstants.ApiAuthDefaultScheme, crypt);
        }

        protected override string GetExpiredToken()
        {
            return "P83hovvDJI9+6LMyV9Tv/BtOITQB7fVvmMl2jvomZW5IpkmGq36VHUlx52Pygkn1+PiypZ6VinBVRdh9BPPX0lLg4kA6vIJjlxRN1h/EF+kx2PSjqlXSfnI0Bu2G1IFqyzQacf2JEPAyaCQuw+M/xQ==";
        }
    }
}
