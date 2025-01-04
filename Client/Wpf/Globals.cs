using Deve.Core;
using Deve.Internal;

namespace Deve.ClientApp.Wpf
{
    internal static class Globals
    {
        #region IData
        private static IData? _data;
        public static IData Data => _data ??= CoreFactory.Get();
        #endregion

        #region UserToken
        public static UserToken? UserToken { get; set; }
        public static string LoggerUser => UserToken?.Subject.Name ?? string.Empty;
        #endregion
    }
}
