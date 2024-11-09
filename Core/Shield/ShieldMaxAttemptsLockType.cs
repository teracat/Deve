namespace Deve.Core.Shield
{
    internal enum ShieldMaxAttemptsLockType
    {
        /// <summary>
        /// Locks only the called method.
        /// </summary>
        OnlyMethod,

        /// <summary>
        /// Locks the origin for all protected methods.
        /// </summary>
        Origin
    }
}
