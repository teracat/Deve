namespace Deve.Repositories;

public interface IRepository;

public interface IRepository<TEntity> : IRepository where TEntity : class
{
    IQueryable<TEntity> GetAsQueryable();

    Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
