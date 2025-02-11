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
        /// <summary>
        /// The SigningSecretKey is used to ensure the integrity and authenticity of the token.
        /// IMPORTANT: You shoud change this value for your own key.
        /// </summary>
        private const string SigningSecretKey = "73b€27f9ce1e4@e789.977cO9feB6ae!";  //Must be 32 bytes

        /// <summary>
        /// The EncryptionSecretKey is used to protect the confidentiality of the token's data.
        /// IMPORTANT: You shoud also change this value for your own key (different from the previous key).
        /// </summary>
        private const string EncryptionSecretKey = "7Gb86@04136b42!a85fa646b73b29f-d";  //Must be 32 bytes

        private readonly byte[] _signingKeyBytes;
        private readonly byte[] _encryptionKeyBytes;

        public TokenManagerJwt()
        {
            _signingKeyBytes = Encoding.ASCII.GetBytes(SigningSecretKey);
            _encryptionKeyBytes = Encoding.ASCII.GetBytes(EncryptionSecretKey);
        }

        public UserToken CreateToken(User user, string scheme = ApiConstants.AuthDefaultScheme)
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

        public TokenParseResult ValidateToken(string token, out UserIdentity? userIdentity)
        {
            userIdentity = null;
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

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                if (principal is null)
                    return TokenParseResult.NotValid;

                userIdentity = UserConverter.ToUserIdentity(principal);

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

        public void Dispose()
        {
        }
    }
}