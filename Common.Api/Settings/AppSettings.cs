namespace Deve.Api.Settings
{
    /// <summary>
    /// Application settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// JWT keys settings.
        /// </summary>
        public AppSettingsJwtKeys JwtKeys { get; set; } = new();

        /// <summary>
        /// DSN for Sentry.
        /// </summary>
        public string SentryDsn { get; set; } = string.Empty;
    }
}