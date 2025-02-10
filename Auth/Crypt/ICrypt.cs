namespace Deve.Auth.Crypt
{
    public interface ICrypt
    {
        string Encrypt(string text);
        string Decrypt(string encryptedText);
    }
}
