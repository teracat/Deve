namespace Deve.Repositories;

public interface IRepositoryRead<out TEntity> : IRepository where TEntity : class
{
    IQueryable<TEntity> GetAsQueryable();
}
