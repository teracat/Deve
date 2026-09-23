using Microsoft.Extensions.Options;
using Deve.Options;

namespace Deve.Customers.Countries;

internal sealed class RepositoryWrite : IRepositoryWrite<Country>
{
    // You should implement real repository logic here
    private static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);

    public RepositoryWrite(IOptions<ConnectionStringsOptions> options)
    {
        // Open connection to database using options.Value.CustomersConnectionWrite
        System.Diagnostics.Debug.WriteLine(options.Value.CustomersConnectionWrite);
    }

    public async Task<Guid> AddAsync(Country entity, CancellationToken cancellationToken)
    {
        return await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is not null)
            {
                return Guid.Empty;
            }

            Data.Countries.Add(entity);
            return entity.Id;
        }, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Country entity, CancellationToken cancellationToken)
    {
        return await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is null)
            {
                return false;
            }

            found.Name = entity.Name;
            found.IsoCode = entity.IsoCode;

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
            return Data.Countries.Remove(found);
        }, cancellationToken);
    }

    private static Country? FindLocal(Guid id) => Data.Countries.FirstOrDefault(x => x.Id == id);
}