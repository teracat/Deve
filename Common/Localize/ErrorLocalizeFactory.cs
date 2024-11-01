namespace Deve.Localize
{
    public static class ErrorLocalizeFactory
    {
        private static IErrorLocalize? _localize;

        public static IErrorLocalize Get() => _localize ??= new ErrorLocalize();
    }
}
