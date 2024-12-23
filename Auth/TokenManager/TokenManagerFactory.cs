﻿namespace Deve.Auth
{
    public static class TokenManagerFactory
    {
        private static readonly Dictionary<string, ITokenManager> _tokenManagers = [];

        /// <summary>
        /// Sets the TokenManager for a scheme.
        /// </summary>
        /// <param name="scheme">The sheme to set the TokenManager for.</param>
        /// <param name="tokenManager">The ITokenManager implementation that will be used for that scheme.</param>
        public static void Set(string scheme, ITokenManager tokenManager)
        {
            lock (_tokenManagers)
            {
                if (!_tokenManagers.TryAdd(scheme, tokenManager))
                    _tokenManagers[scheme] = tokenManager;
            }
        }

        /// <summary>
        /// Get the ITokenManager implementation that will be used for the scheme.
        /// </summary>
        /// <param name="scheme">The sheme to get the ITokenManager.</param>
        /// <returns>The ITokenManager implementation associated to the scheme or the default TokenManager if none is defined.</returns>
        public static ITokenManager Get(string scheme = ApiConstants.AuthDefaultScheme)
        {
            if (_tokenManagers.TryGetValue(scheme, out ITokenManager? tokenManager))
                return tokenManager;

            switch (scheme)
            {
                case ApiConstants.AuthDefaultScheme:
                default:
                    //Return the default TokenManager
                    return new TokenManagerCrypt(new CryptAes());
            }
        }
    }
}
