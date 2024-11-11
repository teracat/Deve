namespace Deve.Auth
{
    public static class TokenManagerFactory
    {
        public static TokenManagers TokenManagers { get; private set; } = new TokenManagers();

        public static ITokenManager Get(string scheme = ApiConstants.ApiAuthDefaultScheme) => TokenManagers.Get(scheme);
    }
}
