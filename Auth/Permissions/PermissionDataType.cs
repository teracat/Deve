namespace Deve.Auth.Permissions
{
    /// <summary>
    /// Defines the types of data for which permissions can be checked.
    /// </summary>
    public enum PermissionDataType
    {
        /// <summary>
        /// Represents a city-related permission.
        /// </summary>
        City,

        /// <summary>
        /// Represents a state-related permission.
        /// </summary>
        State,

        /// <summary>
        /// Represents a country-related permission.
        /// </summary>
        Country,

        /// <summary>
        /// Represents a user-related permission.
        /// </summary>
        User,

        /// <summary>
        /// Represents a client-related permission.
        /// </summary>
        Client,

        /// <summary>
        /// Represents a basic client-related permission.
        /// </summary>
        ClientBasic,

        /// <summary>
        /// Represents a statistics-related permission.
        /// </summary>
        Stats
    }
}