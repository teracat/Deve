namespace Deve
{
    public class UserCredentials
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserCredentials()
        {
        }

        public UserCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
