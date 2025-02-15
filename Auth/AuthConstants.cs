namespace Deve.Auth
{
    public static class AuthConstants
    {
        public static readonly int TokenExpiresInHours  = 24;

        internal const string UserClaimId               = "Id";
        internal const string UserClaimUsername         = "Username";
        internal const string UserClaimRole             = "Role";
    }
}