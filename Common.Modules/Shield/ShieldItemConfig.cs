namespace Deve.Shield;

/// <summary>
/// Default shield item configuration.
/// </summary>
public sealed record ShieldItemConfig
{
    /// <summary>
    /// Maximum number of invalid attempts before locking.
    /// </summary>
    public byte MaxInvalidAttempts { get; init; }

    /// <summary>
    /// Number of seconds to lock after reaching the maximum invalid attempts.
    /// </summary>
    public short LockSeconds { get; init; }

    /// <summary>
    /// Type of lock to apply when maximum attempts are reached.
    /// </summary>
    public ShieldMaxAttemptsLockType MaxAttemptsLockType { get; init; }

    public ShieldItemConfig() : this(5, 300, ShieldMaxAttemptsLockType.Origin) { }

    public ShieldItemConfig(byte maxInvalidAttempts, short lockSeconds) : this(maxInvalidAttempts, lockSeconds, ShieldMaxAttemptsLockType.Origin) { }

    public ShieldItemConfig(byte maxInvalidAttempts) : this(maxInvalidAttempts, 300, ShieldMaxAttemptsLockType.Origin) { }

    public ShieldItemConfig(byte maxInvalidAttempts, short lockSeconds, ShieldMaxAttemptsLockType maxAttemptsLockType)
    {
        MaxInvalidAttempts = maxInvalidAttempts;
        LockSeconds = lockSeconds;
        MaxAttemptsLockType = maxAttemptsLockType;
    }
}
