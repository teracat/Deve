using System.Text.Json;
using Deve.Crypt;
using Deve.Logging;

namespace Deve.Auth.TokenManagers;

/// <summary>
/// Class used to create and validate tokens using an ICrypt implementation to encrypt/decrypt the token content.
/// </summary>
public sealed class TokenManagerCrypt : ITokenManager
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

    ///<inheritdoc/>
    public UserToken CreateToken(UserIdentity identity, string scheme)
    {
        ArgumentNullException.ThrowIfNull(identity);

        var expires = DateTime.UtcNow.AddHours(AuthConstants.TokenExpiresInHours);
        var tokenData = new TokenData(identity, expires);
        var content = JsonSerializer.Serialize(tokenData, _jsonSerializerOptions);
        var token = _crypt.Encrypt(content);
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
            var decrypted = _crypt.Decrypt(token);
            var tokenData = JsonSerializer.Deserialize<TokenData>(decrypted, _jsonSerializerOptions);
            if (tokenData is null || tokenData.Expires < DateTime.UtcNow)
            {
                return false;
            }

            identity = tokenData.Subject;

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
