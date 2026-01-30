namespace Deve.Repositories;

public interface IRepository;

public interface IRepository<TEntity> : IRepository where TEntity : class
{
    IQueryable<TEntity> GetAsQueryable();

    Task<Guid> AddAsync(TEntity entity) => AddAsync(entity, CancellationToken.None);
    Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(TEntity entity) => UpdateAsync(entity, CancellationToken.None);
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid id) => DeleteAsync(id, CancellationToken.None);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
