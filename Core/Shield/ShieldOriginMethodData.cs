namespace Deve.Core.Shield
{
    /// <summary>
    /// Status information about a method for an origin.
    /// </summary>
    internal class ShieldOriginMethodData
    {
        /// <summary>
        /// The current status of the method for the origin.
        /// </summary>
        public ShieldLockStatus Status { get; set; } = ShieldLockStatus.Unlocked;

        /// <summary>
        /// Number of invalid attemps permorfed to the method.
        /// </summary>
        public byte NumAttemptsInvalid { get; set; } = 0;

        /// <summary>
        /// When the last attempt was made.
        /// </summary>
        public DateTimeOffset LastAttempt { get; set; } = DateTimeOffset.UtcNow;
    }
}
