using Microsoft.Extensions.Options;
using Deve.Options;

namespace Deve.MODULE_NAME.FEATURE_PLURAL;

internal sealed class RepositoryWrite : IRepositoryWrite<FEATURE_SINGULAR>
{
    // You should implement real repository logic here
    private static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);

    public RepositoryWrite(IOptions<ConnectionStringsOptions> options)
    {
        // Open connection to database using options.Value.MODULE_NAMEConnectionWrite
        System.Diagnostics.Debug.WriteLine(options.Value.MODULE_NAMEConnectionWrite);
    }

    public async Task<Guid> AddAsync(FEATURE_SINGULAR entity, CancellationToken cancellationToken)
    {
        return await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is not null)
            {
                return Guid.Empty;
            }

            Data.FEATURE_PLURAL.Add(entity);
            return entity.Id;
        }, cancellationToken);
    }

    public async Task<bool> UpdateAsync(FEATURE_SINGULAR entity, CancellationToken cancellationToken)
    {
        return await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is null)
            {
                return false;
            }

            found.Name = entity.Name;

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
            return Data.FEATURE_PLURAL.Remove(found);
        }, cancellationToken);
    }

    private static FEATURE_SINGULAR? FindLocal(Guid id) => Data.FEATURE_PLURAL.FirstOrDefault(x => x.Id == id);
}
