namespace Deve.Auth
{
    public static class AuthConstants
    {
        public const int TokenExpiresInHours        = 24;

        public const string UserClaimId             = "Id";
        public const string UserClaimUsername       = "Username";
        public const string UserClaimRole           = "Role";

        //You should use your own Crypt implementation or, at least, change the Key & IV used to encrypt/decrypt.
        internal const string CryptAesKey          = "68!6c3aScadc4eeba~5c28@ef9f6_3d5"; //len 32
        internal const string CryptAesIV           = "e3@f5/4d6-4e7162";                 //len 16
    }
}
