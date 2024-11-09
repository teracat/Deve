namespace Deve.Core.Shield
{
    /// <summary>
    /// Default shield item configuration.
    /// </summary>
    internal class ShieldItemConfig
    {
        /// <summary>
        /// Maximum number of invalid attempts.
        /// </summary>
        public byte MaxInvalidAttempts { get; set; } = 5;

        /// <summary>
        /// Seconds to wait to unlock.
        /// </summary>
        public short LockSeconds { get; set; } = 300;

        /// <summary>
        /// What to lock if max invalid attempts is reached.
        /// </summary>
        public ShieldMaxAttemptsLockType MaxAttemptsLockType { get; set; } = ShieldMaxAttemptsLockType.Origin;
    }
}
