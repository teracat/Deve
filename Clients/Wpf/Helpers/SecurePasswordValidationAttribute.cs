using System.Security;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;

namespace Deve.Clients.Wpf.Helpers
{
    public class SecurePasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is SecureString securePassword)
            {
                string password = ConvertSecureStringToString(securePassword);
                return !string.IsNullOrWhiteSpace(password);
            }
            return false;
        }

        private static string ConvertSecureStringToString(SecureString secureString)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(ptr) ?? string.Empty;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(ptr);
                }
            }
        }
    }
}
