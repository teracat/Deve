namespace Deve
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserCredentials()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        public UserCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
