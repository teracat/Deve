using System.Text;
using System.Security.Cryptography;

namespace Deve.Auth
{
    internal class Hash : IHash
    {
        public string Calc(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            using (var sha = SHA512.Create())
            {
                var data = Encoding.UTF8.GetBytes(text);
                var hash = sha.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
