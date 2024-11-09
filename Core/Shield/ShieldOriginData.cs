namespace Deve.Core.Shield
{
    /// <summary>
    /// Status information about an origin.
    /// </summary>
    internal class ShieldOriginData
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
}
