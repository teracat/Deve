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
    public class TokenManagerCrypt : ITokenManager
    {
        private readonly ICrypt _crypt;
        private readonly bool _disposeCrypt;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            WriteIndented = false,
        };

        /// <summary>
        /// Constructor. It uses a new CryptAes instance with auto generated Key and IV to encrypt/decrypt data.
        /// </summary>
        public TokenManagerCrypt()
        {
            _crypt = new CryptAes();
            _disposeCrypt = true;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="crypt">Crypt implementation to encrypt/decrypt data.</param>
        /// <param name="autoDisposeCrypt">If true, the crypt will be disposed when this instance is disposed.</param>
        public TokenManagerCrypt(ICrypt crypt, bool autoDisposeCrypt)
        {
            _crypt = crypt;
            _disposeCrypt = autoDisposeCrypt;
        }

        public UserToken CreateToken(User user, string scheme)
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

        public bool TryValidateToken(string token, out UserIdentity? userIdentity)
        {
            userIdentity = null;
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            try
            {
                var decrypted = _crypt.Decrypt(token);
                var tokenData = JsonSerializer.Deserialize<TokenData>(decrypted, _jsonSerializerOptions);
                if (tokenData is null || tokenData.Expires < DateTime.UtcNow)
                {
                    return false;
                }

                userIdentity = tokenData.Subject;

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }

        public void Dispose()
        {
            if (_disposeCrypt)
            {
                _crypt.Dispose();
            }
        }
    }
}