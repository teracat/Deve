namespace Deve.Auth.Permissions
{
    /// <summary>
    /// Defines the types of permissions available for operations.
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// Permission to retrieve a list of items.
        /// </summary>
        GetList,

        /// <summary>
        /// Permission to retrieve a single item.
        /// </summary>
        Get,

        /// <summary>
        /// Permission to add a new item.
        /// </summary>
        Add,

        /// <summary>
        /// Permission to update an existing item.
        /// </summary>
        Update,

        /// <summary>
        /// Permission to delete an item.
        /// </summary>
        Delete
    }

}