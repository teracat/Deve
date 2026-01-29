namespace Deve.Identity.Entities;

internal sealed class User
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public UserStatus Status { get; set; } = UserStatus.Active;

    public string Username { get; set; } = string.Empty;

    public DateTimeOffset Joined { get; set; } = DateTimeOffset.Now;

    public Role Role { get; set; } = Role.User;
    public string? Email { get; set; }
    public DateTime? Birthday { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
}
