namespace Deve.Auth
{
    public static class AuthConstants
    {
        public static readonly int TokenExpiresInHours  = 24;

        internal const string UserClaimId               = "Id";
        internal const string UserClaimUsername         = "Username";
        internal const string UserClaimRole             = "Role";

        //You should use your own Crypt implementation or, at least, change the Key & IV used to encrypt/decrypt.
        internal const string CryptAesKey               = "68!6c3aScadc4eeba~5c28@ef9f6_3d5"; //len 32
        internal const string CryptAesIV                = "e3@f5/4d6-4e7162";                 //len 16
    }
}