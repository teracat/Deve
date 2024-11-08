namespace Deve.Core
{
    /// <summary>
    /// Status of origin.
    /// </summary>
    internal class ShieldOrigin
    {
        /// <summary>
        /// The current status of the origin id.
        /// </summary>
        public ShieldLockStatus Status { get; set; } = ShieldLockStatus.Unlocked;

        /// <summary>
        /// When the last attempt was made by the origin.
        /// </summary>
        public DateTimeOffset LastAttempt { get; set; } = DateTimeOffset.UtcNow;
    }
}
