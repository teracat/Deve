namespace Deve.ClientApp.Maui
{
    internal static class Globals
    {
        #region UserToken
        public static UserToken? UserToken { get; set; }
        public static string LoggerUser => UserToken?.Subject.Name ?? string.Empty;
        #endregion

        #region Helpers
        public static string AppVersion => "v" + AppInfo.Version.ToString(3);
        #endregion
    }
}
