using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Deve.Auth.Mappers;
using Deve.Logging;

namespace Deve.Auth.TokenManagers.Jwt;

/// <summary>
/// Used to create and validate tokens using JWT (JSON Web Tokens).
/// The token is encrypted to protect its data.
/// </summary>
public sealed class JwtTokenManager : ITokenManager
{
    private const string Audience = "Deve";
    private const string Issuer = "Deve";

    private readonly byte[] _signingKeyBytes;
    private readonly byte[] _encryptionKeyBytes;

    /// <summary>
    /// Creates a new instance of TokenManagerJwt.
    /// </summary>
    /// <param name="signingSecretKey">The signingSecretKey is used to ensure the integrity and authentiClient of the token (must be 32 bytes).</param>
    /// <param name="encryptionSecretKey">The encryptionSecretKey is used to protect the confidentiality of the token's data (must be 32 bytes).</param>
    public JwtTokenManager(string signingSecretKey, string encryptionSecretKey)
    {
        _signingKeyBytes = Encoding.ASCII.GetBytes(signingSecretKey);
        _encryptionKeyBytes = Encoding.ASCII.GetBytes(encryptionSecretKey);
    }

    ///<inheritdoc/>
    public UserToken CreateToken(UserIdentity identity, string scheme)
    {
        ArgumentNullException.ThrowIfNull(identity);

        var expires = DateTime.UtcNow.AddHours(AuthConstants.TokenExpiresInHours);
        var claimsIdentity = UserIdentityMapper.ToClaimsIdentity(scheme, identity);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_signingKeyBytes), SecurityAlgorithms.HmacSha256Signature),
            EncryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(_encryptionKeyBytes), SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512),
            Audience = Audience,
            Issuer = Issuer
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);

        return UserToken.Create(token, scheme, expires);
    }

    ///<inheritdoc/>
    public UserToken CreateToken(UserIdentity identity) => CreateToken(identity, AuthConstants.DefaultScheme);

    ///<inheritdoc/>
    public bool TryValidateToken(string token, out UserIdentity? identity)
    {
        identity = null;
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
                ValidAudience = Audience,
                ValidIssuer = Issuer
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken _);
            if (principal is null)
            {
                return false;
            }

            identity = UserIdentityMapper.ToUserIdentity(principal);

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
