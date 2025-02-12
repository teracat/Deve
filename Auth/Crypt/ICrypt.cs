namespace Deve.Auth.Crypt
{
    public interface ICrypt : IDisposable
    {
        string Encrypt(string text);
        string Decrypt(string encryptedText);
    }
}