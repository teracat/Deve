namespace Deve.Shield;

/// <summary>
/// Default shield item configuration.
/// <paramref name="MaxInvalidAttempts"/>Maximum number of invalid attempts before locking.</param>
/// <param name="LockSeconds">Number of seconds to lock after reaching the maximum invalid attempts.</param>
/// <param name="MaxAttemptsLockType">Type of lock to apply when maximum attempts are reached.</param>
/// </summary>
public sealed record ShieldItemConfig(byte MaxInvalidAttempts, short LockSeconds, ShieldMaxAttemptsLockType MaxAttemptsLockType)
{
    public static ShieldItemConfig Create() => new(5, 300, ShieldMaxAttemptsLockType.Origin);

    public static ShieldItemConfig Create(byte maxInvalidAttempts, short lockSeconds) => new(maxInvalidAttempts, lockSeconds, ShieldMaxAttemptsLockType.Origin);

    public static ShieldItemConfig Create(byte maxInvalidAttempts) => new(maxInvalidAttempts, 300, ShieldMaxAttemptsLockType.Origin);
}
