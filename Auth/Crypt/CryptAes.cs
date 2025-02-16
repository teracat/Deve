using System.Text;
using System.Security.Cryptography;

namespace Deve.Auth.Crypt
{
    /// <summary>
    /// Encryption/decryption.
    /// </summary>
    public class CryptAes : ICrypt
    {
        /// <summary>
        /// Cryptographic object that is used to encrypt and decrypt.
        /// </summary>
        private readonly Aes _aes;

        /// <summary>
        /// Default constructor which generates a new Key and IV.
        /// </summary>
        public CryptAes()
        {
            _aes = Aes.Create();
        }

        /// <summary>
        /// Constructor that allows to set the Key & IV.
        /// </summary>
        /// <param name="key">The key used to encrypt/decrypt.</param>
        /// <param name="iv">The IV used to encrypt/decrypt.</param>
        public CryptAes(string key, string iv)
        {
            _aes = Aes.Create();
            _aes.Key = Encoding.UTF8.GetBytes(key);
            _aes.IV = Encoding.UTF8.GetBytes(iv);
        }

        /// <summary>
        /// Encrypts the text.
        /// </summary>
        /// <param name="text">Text to encrypt.</param>
        /// <returns>Encrypted text.</returns>
        public string Encrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] encrypted;
            // Create a new MemoryStream object to contain the encrypted bytes.
            using (var memoryStream = new MemoryStream())
            {
                // Create a CryptoStream object to perform the encryption.
                using (var cryptoStream = new CryptoStream(memoryStream, _aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt the plaintext.
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(text);
                    }

                    encrypted = memoryStream.ToArray();
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Decrypts the text.
        /// </summary>
        /// <param name="text">Text to decrypt.</param>
        /// <returns>Decrypted text.</returns>
        public string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
            {
                return encryptedText;
            }

            string decrypted;
            var bytes = Convert.FromBase64String(encryptedText);

            // Create a new MemoryStream object to contain the decrypted bytes.
            using (var memoryStream = new MemoryStream(bytes))
            {
                // Create a CryptoStream object to perform the decryption.
                using (var cryptoStream = new CryptoStream(memoryStream, _aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    // Decrypt the ciphertext.
                    using (var streamReader = new StreamReader(cryptoStream))
                    {
                        decrypted = streamReader.ReadToEnd();
                    }
                }
            }

            return decrypted;
        }

        public void Dispose()
        {
            _aes.Dispose();
        }
    }
}