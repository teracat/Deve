using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Deve.Authenticate;
using Deve.Logging;
using Deve.Auth.Converters;
using Deve.Internal.Model;

namespace Deve.Auth.TokenManagers.Jwt
{
    /// <summary>
    /// Class used to create and validate tokens using JWT (JSON Web Tokens).
    /// The token is encrypted to protect its data.
    /// </summary>
    public class TokenManagerJwt : ITokenManager
    {
        private readonly byte[] _signingKeyBytes;
        private readonly byte[] _encryptionKeyBytes;

        /// <summary>
        /// Creates a new instance of TokenManagerJwt.
        /// </summary>
        /// <param name="signingSecretKey">The signingSecretKey is used to ensure the integrity and authenticity of the token (must be 32 bytes).</param>
        /// <param name="encryptionSecretKey">The encryptionSecretKey is used to protect the confidentiality of the token's data (must be 32 bytes).</param>
        public TokenManagerJwt(string signingSecretKey, string encryptionSecretKey)
        {
            _signingKeyBytes = Encoding.ASCII.GetBytes(signingSecretKey);
            _encryptionKeyBytes = Encoding.ASCII.GetBytes(encryptionSecretKey);
        }

        public UserToken CreateToken(User user, string scheme)
        {
            ArgumentNullException.ThrowIfNull(user);

            var expires = DateTime.UtcNow.AddHours(AuthConstants.TokenExpiresInHours);
            var subject = UserConverter.ToUserSubject(user);
            var userIdentity = new UserIdentity(user);
            var claimsIdentity = UserConverter.ToClaimsIdentity(scheme, userIdentity);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_signingKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                EncryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(_encryptionKeyBytes), SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new UserToken(subject, expires, token, scheme);
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
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_signingKeyBytes),
                    TokenDecryptionKey = new SymmetricSecurityKey(_encryptionKeyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                if (principal is null)
                {
                    return false;
                }

                userIdentity = UserConverter.ToUserIdentity(principal);

                return true;
            }
            catch (SecurityTokenExpiredException)
            {
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}