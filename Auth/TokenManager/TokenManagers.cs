namespace Deve.Auth
{
    public class TokenManagers
    {
        private readonly Dictionary<string, ITokenManager> _tokenManagers = [];

        public void Add(string scheme, ITokenManager tokenManager)
        {
            if (_tokenManagers.ContainsKey(scheme))
                _tokenManagers[scheme] = tokenManager;
            else
                _tokenManagers.Add(scheme, tokenManager);
        }

        public void Remove(string scheme)
        {
            _tokenManagers.Remove(scheme);
        }

        public ITokenManager Get(string scheme)
        {
            if (_tokenManagers.TryGetValue(scheme, out ITokenManager? tokenManager))
                return tokenManager;
            
            switch (scheme)
            {
                case ApiConstants.ApiAuthCryptAesScheme:
                case ApiConstants.ApiAuthDefaultScheme:
                default:
                    //Return the Default TokenManager
                    return new TokenManagerCrypt(scheme, new CryptAes());
            }
        }
    }
}