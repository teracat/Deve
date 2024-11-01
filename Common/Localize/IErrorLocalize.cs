namespace Deve.Localize
{
    public interface IErrorLocalize
    {
        string Localize(ResultErrorType errorType, string langCode);
    }
}
