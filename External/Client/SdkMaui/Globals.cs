using Deve.Sdk;

namespace Deve.External.ClientApp.Maui
{
    internal static class Globals
    {
        #region IData
        private static IData? _data;
        public static IData Data => _data ??= Sdk.SdkFactory.Get(EnvironmentType.Staging, null, new LoggingHandlerLog());
        #endregion

        #region UserToken
        public static UserToken? UserToken { get; set; }
        public static string LoggerUser => UserToken?.Subject.Name ?? string.Empty;
        #endregion

        #region Helpers
        public static string AppVersion => "v" + AppInfo.Version.ToString(3);
        #endregion
    }
}
