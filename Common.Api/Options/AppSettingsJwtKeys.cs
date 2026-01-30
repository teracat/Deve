namespace Deve.Api.Options;

/// <summary>
/// JWT keys settings.
/// </summary>
public class AppSettingsJwtKeys
{
    /// <summary>
    /// The secret key used to sign the JWT token.
    /// </summary>
    public string SigningSecretKey { get; set; } = string.Empty;

    /// <summary>
    /// The secret key used to encrypt the JWT token.
    /// </summary>
    public string EncryptionSecretKey { get; set; } = string.Empty;
}