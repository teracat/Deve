using System.Text;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Deve.Auth.Jwt
{
    internal class TokenManagerJwt : ITokenManager
    {
        /// <summary>
        /// The SigningSecretKey is used to ensure the integrity and authenticity of the token.
        /// You shoud change this value for your own key.
        /// </summary>
        private const string SigningSecretKey = "73be27f9ce1e4ae7899977c09feb6aef";  //Must be 32 bytes

        /// <summary>
        /// The EncryptionSecretKey is used to protect the confidentiality of the token's data.
        /// You shoud also change this value for your own key (different from the previous key).
        /// </summary>
        private const string EncryptionSecretKey = "76b86004136b421a85fa646b73b29f6d";  //Must be 32 bytes

        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            WriteIndented = false,
        };
        private readonly string _scheme;
        private readonly byte[] _signingKeyBytes;
        private readonly byte[] _encryptionKeyBytes;

        public TokenManagerJwt(string scheme)
        {
            _scheme = scheme;
            _signingKeyBytes = Encoding.ASCII.GetBytes(SigningSecretKey);
            _encryptionKeyBytes = Encoding.ASCII.GetBytes(EncryptionSecretKey);
        }

        public UserToken CreateToken(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var expires = DateTime.UtcNow.AddHours(AuthConstants.TokenExpiresInHours);
            var tokenData = new TokenData(user, expires);
            var subject = UserConverter.ToUserSubject(user);
            var identity = UserConverter.ToClaimsIdentity(_scheme, tokenData);
            var content = JsonSerializer.Serialize(tokenData, _jsonSerializerOptions);

            identity.AddClaim(new System.Security.Claims.Claim(AuthConstants.UserClaimContent, content));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_signingKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                EncryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(_encryptionKeyBytes), SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new UserToken(subject, expires, token, _scheme);
        }

        public TokenParseResult ValidateToken(string token, out TokenData? tokenData)
        {
            tokenData = null;
            if (string.IsNullOrWhiteSpace(token))
                return TokenParseResult.NotValid;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_signingKeyBytes),
                    TokenDecryptionKey = new SymmetricSecurityKey(_encryptionKeyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                if (principal is null)
                    return TokenParseResult.NotValid;

                var content = principal.Claims.FirstOrDefault(c => c.Type == AuthConstants.UserClaimContent)?.Value;
                if (string.IsNullOrEmpty(content))
                    return TokenParseResult.NotValid;

                tokenData = JsonSerializer.Deserialize<TokenData>(content, _jsonSerializerOptions);
                if (tokenData is null)
                    return TokenParseResult.NotValid;

                if (tokenData.Expires < DateTime.UtcNow)
                    return TokenParseResult.Expired;

                return TokenParseResult.Valid;
            }
            catch (SecurityTokenExpiredException)
            {
                return TokenParseResult.Expired;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return TokenParseResult.NotValid;
            }
        }
    }

    public static class TokenManagerJwtExtension
    {
        private static TokenManagerJwt? _instance;

        public static void AddJwt(this TokenManagers tokenManagers, string scheme)
        {
            if (_instance is null)
            {
                _instance = new TokenManagerJwt(scheme);
                tokenManagers.Add(scheme, _instance);
            }
        }

        public static void RemoveJwt(this TokenManagers tokenManagers, string scheme)
        {
            if (_instance is not null)
            {
                tokenManagers.Remove(scheme);
                _instance = null;
            }
        }
    }
}