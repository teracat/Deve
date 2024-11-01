namespace Deve
{
    public class User : ModelId
    {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime Joined { get; set; } = DateTime.Today;

        public Role Role { get; set; } = Role.User;
        public string? Email { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
