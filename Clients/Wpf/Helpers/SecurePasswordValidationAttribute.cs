using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security;

namespace Deve.Clients.Wpf.Helpers;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
internal sealed class SecurePasswordValidationAttribute : ValidationAttribute
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
