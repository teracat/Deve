namespace Deve.Internal.Dto
{
    /// <summary>
    /// The User information with Plain Password.
    /// </summary>
    public class UserPlainPassword : UserBase
    {
        public string? Password { get; set; }

        public UserPlainPassword()
        {
        }

        public UserPlainPassword(UserBase other)
            : base(other)
        {
        }
    }
}
