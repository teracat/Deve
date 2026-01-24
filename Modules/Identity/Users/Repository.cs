namespace Deve.Identity.Users;

internal sealed class Repository : IRepository<User>
{
    private static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);

    public IQueryable<User> GetAsQueryable() => Data.Users.AsQueryable();

    public async Task<Guid> AddAsync(User entity, CancellationToken cancellationToken)
    {
        return await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            else
            {
                var found = FindLocal(entity.Id);
                if (found is null)
                {
                    return Guid.Empty;
                }
            }

            Data.Users.Add(entity);
            return entity.Id;
        }, cancellationToken);
    }

    public async Task<bool> UpdateAsync(User entity, CancellationToken cancellationToken)
    {
        return await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is null)
            {
                return false;
            }

            found.Name = entity.Name;
            found.Username = entity.Username;
            found.Role = entity.Role;
            found.Email = entity.Email;
            found.Birthday = entity.Birthday;
            found.Joined = entity.Joined;
            found.Status = entity.Status;
            found.PasswordHash = entity.PasswordHash;

            return true;
        }, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(id);
            if (found is null)
            {
                return false;
            }
            return Data.Users.Remove(found);
        }, cancellationToken);
    }

    private static User? FindLocal(Guid id) => Data.Users.FirstOrDefault(x => x.Id == id);
}