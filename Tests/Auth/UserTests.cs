namespace Deve.Tests.Auth
{
    internal class UserTests : User
    {
        public UserTests()
        {
            Id = 1;
            Name = "Tests User";
            IsActive = true;
            Username = "testsuser";
            PasswordHash = "";
            Joined = new DateTime(2024, 10, 28);
            Role = Role.User;
        }
    }
}
