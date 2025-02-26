namespace Deve.Auth.Permissions
{
    /// <summary>
    /// Represents the result of a permission check.
    /// </summary>
    public enum PermissionResult
    {
        /// <summary>
        /// The user is not authorized to perform the operation (e.g., not authenticated).
        /// </summary>
        Unauthorized,

        /// <summary>
        /// The user is authenticated but does not have the required permission.
        /// </summary>
        NotGranted,

        /// <summary>
        /// The user has the required permission.
        /// </summary>
        Granted
    }
}