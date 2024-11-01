using System.Text.Json;

namespace Deve.Auth
{
    internal class TokenManager : ITokenManager
    {
        private readonly ICrypt _crypt;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = false,
        };

        public TokenManager(ICrypt crypt)
        {
            _crypt = crypt;
        }

        public UserToken CreateToken(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var expires = DateTime.UtcNow.AddHours(AuthConstants.TokenExpiresInHours);
            var tokenData = new TokenData(user, expires);
            var content = JsonSerializer.Serialize(tokenData, _jsonSerializerOptions);
            var token = _crypt.Encrypt(content);
            var subject = UserConverter.ToUserSubject(user);
            return new UserToken(subject, expires, token, ApiConstants.ApiAuthDefaultScheme);
        }

        public TokenParseResult ValidateToken(string token, out TokenData? tokenData)
        {
            tokenData = null;
            if (string.IsNullOrWhiteSpace(token))
                return TokenParseResult.NotValid;

            try
            {
                var decrypted = _crypt.Decrypt(token);
                tokenData = JsonSerializer.Deserialize<TokenData>(decrypted, _jsonSerializerOptions);
                if (tokenData is null)
                    return TokenParseResult.NotValid;

                if (tokenData.Expires < DateTime.UtcNow)
                    return TokenParseResult.Expired;

                return TokenParseResult.Valid;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return TokenParseResult.NotValid;
            }
        }
    }
}