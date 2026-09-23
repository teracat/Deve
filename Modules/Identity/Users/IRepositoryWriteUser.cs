namespace Deve.Identity.Users;

internal interface IRepositoryWriteUser : IRepositoryWrite<User>
{
    Task<bool> UpdatePasswordAsync(Guid id, string passwordHash) => UpdatePasswordAsync(id, passwordHash, CancellationToken.None);
    Task<bool> UpdatePasswordAsync(Guid id, string passwordHash, CancellationToken cancellationToken);
}
