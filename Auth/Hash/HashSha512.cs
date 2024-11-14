using System.Text;
using System.Security.Cryptography;

namespace Deve.Auth
{
    internal class HashSha512 : IHash
    {
        public string Calc(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var data = Encoding.UTF8.GetBytes(text);
            var hash = SHA512.HashData(data);
            return Convert.ToBase64String(hash);
        }
    }
}
