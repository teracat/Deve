using Microsoft.AspNetCore.DataProtection;
using Deve.Auth.Crypt;

namespace Deve.Api.Crypt
{
    /// <summary>
    /// Provides encryption and decryption using IDataProtector.
    /// </summary>
    public class CryptDataProtect : ICrypt
    {
        /// <summary>
        /// The data protector instance used for encryption and decryption.
        /// </summary>
        private readonly IDataProtector _protector;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="provider">The data protection provider used to create a protector.</param>
        public CryptDataProtect(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector(nameof(CryptDataProtect));
        }

        /// <summary>
        /// Encrypts the specified text using data protection.
        /// </summary>
        /// <param name="text">The plain text to encrypt.</param>
        /// <returns>The encrypted text.</returns>
        public string Encrypt(string text) => _protector.Protect(text);

        /// <summary>
        /// Decrypts the specified encrypted text using data protection.
        /// </summary>
        /// <param name="encryptedText">The encrypted text to decrypt.</param>
        /// <returns>The decrypted plain text.</returns>
        public string Decrypt(string encryptedText) => _protector.Unprotect(encryptedText);

        /// <summary>
        /// Releases any resources used by the instance. (Not needed for this implementation).
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}