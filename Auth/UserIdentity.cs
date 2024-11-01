namespace Deve.Auth
{
    /// <summary>
    /// User information used internally (can contain private information).
    /// </summary>
    public class UserIdentity
    {
        /// <summary>
        /// Unique identifier of the user.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Role of the user (to check permissions)
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Creates instance with default values.
        /// </summary>
        public UserIdentity()
        {
            Id = 0;
            Role = Role.User;
        }

        /// <summary>
        /// Create instance and copies the values from this User.
        /// </summary>
        /// <param name="user">User to copy data from.</param>
        public UserIdentity(User user)
        {
            Id = user.Id;
            Role = user.Role;
        }
    }
}