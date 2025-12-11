using Deve.Dto;

namespace Deve.Internal.Dto
{
    /// <summary>
    /// User base class.
    /// </summary>
    public class UserBase : ModelId
    {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public string Username { get; set; } = string.Empty;

        public DateTime Joined { get; set; } = DateTime.Today;

        public Role Role { get; set; } = Role.User;
        public string? Email { get; set; }
        public DateTime? Birthday { get; set; }

        public UserBase()
        {
        }

        public UserBase(UserBase other)
            : base(other)
        {
            Name = other.Name;
            IsActive = other.IsActive;
            Username = other.Username;
            Joined = other.Joined;
            Role = other.Role;
            Email = other.Email;
            Birthday = other.Birthday;
        }
    }
}
