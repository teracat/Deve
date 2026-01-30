namespace Deve.Identity;

internal static class Data
{
    //Users
    private static readonly Guid TeracatUserId = Guid.Parse("3c4d5e6f-7081-9201-2345-67890abcdef2");
    private static readonly Guid DanBrownUserId = Guid.Parse("4d5e6f70-8192-0123-4567-890abcdef123");

    public static readonly List<User> Users =
    [
        new User() { Id = TeracatUserId, Role = Role.Admin, Name = "Jordi Badia", Username = "teracat", Email = "jordib@teracat.com", Status = UserStatus.Active, PasswordHash = "Rke4hgrN46Zgzeivca85NdtghI7CL3yFtbzoLTxHe0HYsEUscTRH8TtJw1Si6z+/krDvdzSBoCLWs55slzOV3Q==" },
        new User() { Id = DanBrownUserId, Role = Role.User, Name = "Dan Brown", Username = "dan.brown",  Status = UserStatus.Inactive, PasswordHash = "9jn9NVRbBwRdo0/+5c63F6pO77Jzc8Der3nH8vyDiHjunLrFefqlkbf55TF7SS+LhCrDj20bt77LxPetqLaYWA==" },
    ];
}
