namespace Deve.Authenticate
{
    /// <summary>
    /// Represents user credentials containing a username and password.
    /// </summary>
    public class UserCredentials
    {
        /// <summary>
        /// Gets or sets the username associated with the user credentials.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password associated with the user credentials.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Initializes a new instance with empty values.
        /// </summary>
        public UserCredentials()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance with the specified username and password.
        /// </summary>
        /// <param name="username">The username to assign.</param>
        /// <param name="password">The password to assign.</param>
        public UserCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}