namespace Deve.Core
{
    /// <summary>
    /// Information of the shield method accessed by a device.
    /// </summary>
    internal class ShieldItem
    {
        /// <summary>
        /// Status of the Lock for the method.
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
