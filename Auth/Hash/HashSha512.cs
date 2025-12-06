using System.Security.Cryptography;
using System.Text;

namespace Deve.Auth.Hash
{
    /// <summary>
    /// Provides SHA-512 hash calculation.
    /// </summary>
    public class HashSha512 : IHash
    {
        /// <summary>
        /// Computes the SHA-512 hash of the given text.
        /// </summary>
        /// <param name="text">The input text to hash.</param>
        /// <returns>
        /// The computed hash as a Base64-encoded string. 
        /// If the input is null or empty, it returns the original text.
        /// </returns>
        public string Calc(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            var data = Encoding.UTF8.GetBytes(text);
            var hash = SHA512.HashData(data);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Releases any resources used by the instance. (Not needed for this implementation).
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}
