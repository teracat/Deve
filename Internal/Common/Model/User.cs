namespace Deve
{
    /// <summary>
    /// User with the password hash.
    /// </summary>
    public class User : UserBase
    {
        public string PasswordHash { get; set; } = string.Empty;

        public User()
        {
        }

        public User(UserBase other)
            : base(other)
        {
        }
    }
}
