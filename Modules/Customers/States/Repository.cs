namespace Deve.Customers.States;

internal sealed class Repository : IRepository<State>
{
    private static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);

    public IQueryable<State> GetAsQueryable() => Data.States.AsQueryable();

    public async Task<Guid> AddAsync(State entity, CancellationToken cancellationToken = default)
    {
        return await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is not null)
            {
                return Guid.Empty;
            }

            Data.States.Add(entity);
            return entity.Id;
        }, cancellationToken);
    }

    public async Task<bool> UpdateAsync(State entity, CancellationToken cancellationToken = default)
    {
        return await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is null)
            {
                return false;
            }

            found.Name = entity.Name;
            found.CountryId = entity.CountryId;

            return true;
        }, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(id);
            if (found is null)
            {
                return false;
            }
            return Data.States.Remove(found);
        }, cancellationToken);
    }

    private static State? FindLocal(Guid id) => Data.States.FirstOrDefault(x => x.Id == id);
}