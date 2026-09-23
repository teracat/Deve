using Moq;
using Deve.Identity.Entities;
using Deve.Identity.Users;

namespace Deve.Tests.Mocks.Repository;

internal class UserRepositoryWriteMock : Mock<IRepositoryWriteUser>
{
    public UserRepositoryWriteMock()
    {
        _ = Setup(d => d.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).Returns<User, CancellationToken>((_, _) => Task.FromResult(Guid.NewGuid()));
        _ = Setup(d => d.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).Returns<User, CancellationToken>((user, _) => Task.FromResult(user.Id != Guid.Empty));
        _ = Setup(d => d.UpdatePasswordAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns<Guid, string, CancellationToken>((id, _, _) => Task.FromResult(id != Guid.Empty));
        _ = Setup(d => d.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).Returns<Guid, CancellationToken>((id, _) => Task.FromResult(id != Guid.Empty));
    }
}
