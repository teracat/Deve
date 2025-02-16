using Microsoft.AspNetCore.DataProtection;
using Deve.Auth.Crypt;

namespace Deve.Api.Crypt
{
    public class CryptDataProtect : ICrypt
    {
        private readonly IDataProtector _protector;

        public CryptDataProtect(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector(nameof(CryptDataProtect));
        }

        public string Encrypt(string text) => _protector.Protect(text);

        public string Decrypt(string encryptedText) => _protector.Unprotect(encryptedText);

        public void Dispose()
        {
            // Nothing to dispose
        }
    }
}