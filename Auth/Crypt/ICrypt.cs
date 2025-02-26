namespace Deve.Auth.Crypt
{
    /// <summary>
    /// Provides encryption and decryption functionality.
    /// </summary>
    public interface ICrypt : IDisposable
    {
        /// <summary>
        /// Encrypts the given text.
        /// </summary>
        /// <param name="text">The plain text to encrypt.</param>
        /// <returns>The encrypted text as a string.</returns>
        string Encrypt(string text);

        /// <summary>
        /// Decrypts the given encrypted text.
        /// </summary>
        /// <param name="encryptedText">The encrypted text to decrypt.</param>
        /// <returns>The decrypted plain text.</returns>
        string Decrypt(string encryptedText);
    }
}