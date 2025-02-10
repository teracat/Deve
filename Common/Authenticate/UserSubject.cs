namespace Deve.Authenticate
{
    /// <summary>
    /// Class included in the Login response with some public user information.
    /// When changed, you should also change the UserConverter. method.
    /// </summary>
    public class UserSubject
    {
        /// <summary>
        /// User name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// When the user joined the platform.
        /// </summary>
        public DateTime Joined { get; set; }

        /// <summary>
        /// Create instance with the default values.
        /// </summary>
        public UserSubject()
        {
            Name = string.Empty;
            Username = string.Empty;
            Joined = DateTime.Now;
        }

        /// <summary>
        /// Create a copy.
        /// </summary>
        /// <param name="other">The other object to copy the data from.</param>
        public UserSubject(UserSubject other)
        {
            Name = other.Name;
            Username = other.Username;
            Joined = other.Joined;
        }
    }
}