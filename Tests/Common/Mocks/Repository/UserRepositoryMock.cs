using Moq;
using Deve.Repositories;
using Deve.Identity.Entities;
using Deve.Identity.Enums;

namespace Deve.Tests.Mocks.Repository;

internal class UserRepositoryMock : Mock<IRepository<User>>
{
    // User with Username "tests" will be used as the user to perform valid logins (it must have the Admin role to pass the permissions checks and it also must be active).
    // User with Username "tests2" will be used for inactive user tests.
    internal readonly IList<User> _data =
    [
        new User() { Id = TestsConstants.ValidUserId, Role = Role.Admin, Name = "Valid Tests User", Username = "tests", Status = UserStatus.Active, Joined = new DateTimeOffset(2024, 9, 19, 12, 0, 0, TimeSpan.Zero), PasswordHash = "Xyj7C/PnP76tl0K4fkui6WZ4paWPZmzItX9wxGqQ8xJta6gFNDGCm4D5bMDTgT+f5lhbgZ31pwE6k0OwdGh+cg==" },
        new User() { Id = TestsConstants.InactiveUserId, Role = Role.User, Name = "Inactive Tests User", Username = "tests2",  Status = UserStatus.Inactive, Joined = new DateTimeOffset(2024, 9, 19, 12, 0, 0, TimeSpan.Zero), PasswordHash = "vaZLjDLI1HmTSVpN6AJEmpxgQdch6uHpMhZOcKLBPLHxkBEDEnds6hfkZCP4/g/UcbTqiZ1vgJBvzX3w3WWOtg==" },
        new User() { Id = TestsConstants.FakeUserId, Role = Role.User, Name = "Fake User", Username = "fake",  Status = UserStatus.Inactive, Joined = new DateTimeOffset(2024, 11, 19, 12, 0, 0, TimeSpan.Zero), PasswordHash = "9jn9NVRbBwRdo0/+5c63F6pO77Jzc8Der3nH8vyDiHjunLrFefqlkbf55TF7SS+LhCrDj20bt77LxPetqLaYWA==" },
        new User() { Id = TestsConstants.UpdateUserId, Role = Role.User, Name = "Fake User 2", Username = "fake2",  Status = UserStatus.Inactive, Joined = new DateTimeOffset(2026, 1, 9, 8, 0, 0, TimeSpan.Zero), PasswordHash = "9jn9NVRbBwRdo0/+5c63F6pO77Jzc8Der3nH8vyDiHjunLrFefqlkbf55TF7SS+LhCrDj20bt77LxPetqLaYWA==" },
    ];

    public UserRepositoryMock()
    {
        _ = Setup(d => d.GetAsQueryable()).Returns(() => _data.AsQueryable());
        _ = Setup(d => d.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).Returns<User, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).Returns<User, CancellationToken>((user, _) => Task.FromResult(user.Id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
