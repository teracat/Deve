namespace Deve.Customers.Cities;

internal sealed class Repository : IRepository<City>
{
    private static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);

    public IQueryable<City> GetAsQueryable() => Data.Cities.AsQueryable();

    public async Task<Guid> AddAsync(City entity, CancellationToken cancellationToken = default) =>
        await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is not null)
            {
                return Guid.Empty;
            }

            Data.Cities.Add(entity);
            return entity.Id;
        }, cancellationToken);

    public async Task<bool> UpdateAsync(City entity, CancellationToken cancellationToken = default) =>
        await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is null)
            {
                return false;
            }

            found.Name = entity.Name;
            found.StateId = entity.StateId;

            return true;
        }, cancellationToken);

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(id);
            if (found is null)
            {
                return false;
            }
            return Data.Cities.Remove(found);
        }, cancellationToken);

    private static City? FindLocal(Guid id) => Data.Cities.FirstOrDefault(x => x.Id == id);
}