namespace Deve.Auth.Hash
{
    public interface IHash : IDisposable
    {
        string Calc(string text);
    }
}