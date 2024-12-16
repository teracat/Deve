namespace Deve.External.ClientApp.Maui
{
    internal static class Globals
    {
        #region IData
        private static IData? _data;
        public static IData Data => _data ??= Sdk.SdkFactory.Get(Deve.Sdk.EnvironmentType.Staging);
        #endregion

        #region Helpers
        public static string AppVersion => DeviceInfo.Current.Version.ToString(3);
        #endregion
    }
}
