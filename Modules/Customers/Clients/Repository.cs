namespace Deve.Customers.Clients;

internal sealed class Repository : IRepository<Client>
{
    private static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);

    public IQueryable<Client> GetAsQueryable() => Data.Clients.AsQueryable();

    public async Task<Guid> AddAsync(Client entity, CancellationToken cancellationToken = default) =>
        await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is not null)
            {
                return Guid.Empty;
            }

            Data.Clients.Add(entity);
            return entity.Id;
        }, cancellationToken);

    public async Task<bool> UpdateAsync(Client entity, CancellationToken cancellationToken = default) =>
        await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is null)
            {
                return false;
            }

            found.Name = entity.Name;
            found.CityId = entity.CityId;

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
            return Data.Clients.Remove(found);
        }, cancellationToken);

    private static Client? FindLocal(Guid id) => Data.Clients.FirstOrDefault(x => x.Id == id);
}