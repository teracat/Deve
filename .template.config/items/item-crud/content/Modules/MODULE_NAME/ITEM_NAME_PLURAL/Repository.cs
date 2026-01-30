using Microsoft.Extensions.Options;
using Deve.Api.Options;

namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL;

internal sealed class Repository : IRepository<ITEM_NAME_SINGULAR>
{
    // You should implement real repository logic here
    private static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);

    private static readonly List<ITEM_NAME_SINGULAR> _data =
    [
        new ITEM_NAME_SINGULAR() { Id = Guid.NewGuid(), Name = "ITEM_NAME_SINGULAR 1" },
        new ITEM_NAME_SINGULAR() { Id = Guid.NewGuid(), Name = "ITEM_NAME_SINGULAR 2" },
    ];

    public Repository(IOptions<ConnectionStringsOptions> options)
    {
        // Open connection to database using options.Value.MODULE_NAMEConnection
        System.Diagnostics.Debug.WriteLine(options.Value.MODULE_NAMEConnection);
    }

    public IQueryable<ITEM_NAME_SINGULAR> GetAsQueryable() => _data.AsQueryable();

    public async Task<Guid> AddAsync(ITEM_NAME_SINGULAR entity, CancellationToken cancellationToken)
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

    public async Task<bool> UpdateAsync(ITEM_NAME_SINGULAR entity, CancellationToken cancellationToken)
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

    private static ITEM_NAME_SINGULAR? FindLocal(Guid id) => _data.FirstOrDefault(x => x.Id == id);
}
