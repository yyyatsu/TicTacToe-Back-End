using System.Linq.Expressions;

namespace TTT.Data.Repository
{
  public interface IRepository<T> where T : class
  {
    IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
    ValueTask<T?> GetByKey(object[] keys);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> RemoveAsync(T entity);
    Task<int> SaveChangesAsync();
  }
}
