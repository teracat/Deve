using Microsoft.Extensions.Options;
using Deve.Api.Options;

namespace Deve.MODULE_NAME.FEATURE_PLURAL;

internal sealed class Repository : IRepository<FEATURE_SINGULAR>
{
    // You should implement real repository logic here
    private static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);

    private static readonly List<FEATURE_SINGULAR> _data =
    [
        new FEATURE_SINGULAR() { Id = Guid.NewGuid(), Name = "FEATURE_SINGULAR 1" },
        new FEATURE_SINGULAR() { Id = Guid.NewGuid(), Name = "FEATURE_SINGULAR 2" },
    ];

    public Repository(IOptions<ConnectionStringsOptions> options)
    {
        // Open connection to database using options.Value.MODULE_NAMEConnection
        System.Diagnostics.Debug.WriteLine(options.Value.MODULE_NAMEConnection);
    }

    public IQueryable<FEATURE_SINGULAR> GetAsQueryable() => _data.AsQueryable();

    public async Task<Guid> AddAsync(FEATURE_SINGULAR entity, CancellationToken cancellationToken)
    {
        return await Utils.RunProtectedAsync(Semaphore, (_) =>
        {
            var found = FindLocal(entity.Id);
            if (found is not null)
            {
                return Guid.Empty;
            }

            _data.Add(entity);
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
            return _data.Remove(found);
        }, cancellationToken);
    }

    private static FEATURE_SINGULAR? FindLocal(Guid id) => _data.FirstOrDefault(x => x.Id == id);
}
