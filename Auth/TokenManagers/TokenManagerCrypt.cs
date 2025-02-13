using System.Text.Json;
using Deve.Logging;
using Deve.Authenticate;
using Deve.Auth.Converters;
using Deve.Auth.Crypt;
using Deve.Internal.Model;

namespace Deve.Auth.TokenManagers
{
    /// <summary>
    /// Class used to create and validate tokens using an ICrypt implementation to encrypt/decrypt the token content.
    /// </summary>
    internal class TokenManagerCrypt : ITokenManager
    {
        private readonly ICrypt _crypt;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            WriteIndented = false,
        };

        public TokenManagerCrypt(ICrypt crypt)
        {
            _crypt = crypt;
        }

        public UserToken CreateToken(User user, string scheme = ApiConstants.AuthDefaultScheme)
        {
            ArgumentNullException.ThrowIfNull(user);

            var expires = DateTime.UtcNow.AddHours(AuthConstants.TokenExpiresInHours);
            var tokenData = new TokenData(user, expires);
            var content = JsonSerializer.Serialize(tokenData, _jsonSerializerOptions);
            var token = _crypt.Encrypt(content);
            var subject = UserConverter.ToUserSubject(user);
            return new UserToken(subject, expires, token, ApiConstants.AuthDefaultScheme);
        }

        public UserToken CreateToken(User user) => CreateToken(user, ApiConstants.AuthDefaultScheme);

        public TokenParseResult ValidateToken(string token, out UserIdentity? userIdentity)
        {
            userIdentity = null;
            if (string.IsNullOrWhiteSpace(token))
            {
                return TokenParseResult.NotValid;
            }

            try
            {
                var decrypted = _crypt.Decrypt(token);
                var tokenData = JsonSerializer.Deserialize<TokenData>(decrypted, _jsonSerializerOptions);
                if (tokenData is null)
                {
                    return TokenParseResult.NotValid;
                }

                if (tokenData.Expires < DateTime.UtcNow)
                {
                    return TokenParseResult.Expired;
                }

                userIdentity = tokenData.Subject;

                return TokenParseResult.Valid;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return TokenParseResult.NotValid;
            }
        }

        public void Dispose()
        {
        }
    }
}