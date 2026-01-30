namespace Deve.Api.Options;

/// <summary>
/// Application settings.
/// </summary>
public class AppSettingsOptions
{
    /// <summary>
    /// JWT keys settings.
    /// </summary>
    public AppSettingsJwtKeys JwtKeys { get; set; } = new();
}