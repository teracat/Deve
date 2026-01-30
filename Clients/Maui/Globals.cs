using Deve.Auth.Login;

namespace Deve.Clients.Maui;

internal static class Globals
{
    #region UserToken
    public static LoginResponse? LoginResponseData { get; set; }
    public static string LoggerUser => LoginResponseData?.Name ?? string.Empty;
    #endregion

    #region Helpers
    public static string AppVersion => "v" + AppInfo.Version.ToString(3);
    #endregion
}
