using Microsoft.Extensions.Options;
using Deve.Options;

namespace Deve.Customers.Clients;

internal sealed class RepositoryWrite : IRepositoryWrite<Client>
{
    // You should implement real repository logic here
    private static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);

    public RepositoryWrite(IOptions<ConnectionStringsOptions> options)
    {
        // Open connection to database using options.Value.CustomersConnectionWrite
        System.Diagnostics.Debug.WriteLine(options.Value.CustomersConnectionWrite);
    }

    public async Task<Guid> AddAsync(Client entity, CancellationToken cancellationToken) =>
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

    public async Task<bool> UpdateAsync(Client entity, CancellationToken cancellationToken) =>
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

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
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