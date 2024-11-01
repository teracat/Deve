namespace Deve.Auth
{
    public interface ICrypt
    {
        string Encrypt(string text);
        string Decrypt(string encryptedText);
    }
}
