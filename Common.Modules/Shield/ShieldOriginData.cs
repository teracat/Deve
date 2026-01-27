namespace Deve.Shield;

/// <summary>
/// Status information about an origin.
/// </summary>
internal sealed class ShieldOriginData
{
    /// <summary>
    /// The current status of the origin.
    /// </summary>
    public ShieldLockStatus Status { get; set; } = ShieldLockStatus.Unlocked;

    /// <summary>
    /// When the last attempt was made by the origin.
    /// </summary>
    public DateTimeOffset LastAttempt { get; set; } = DateTimeOffset.UtcNow;
}
