using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TTT.Data.Context;

namespace TTT.Data.Repository
{
  public sealed class Repository<T> : IRepository<T> where T : class
  {
    private readonly TTTContext context;

	public Repository(TTTContext context)
	{
	  this.context = context;
	}

    public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
    {
      var set = context.Set<T>();

      var includingSet = includes.Aggregate<Expression<Func<T, object>>, IQueryable<T>>(set, (current, include) =>
        current.Include(include));

      return includingSet ?? set;
    }

    public ValueTask<T?> GetByKey(params object[] keys) => context.Set<T>().FindAsync(keys);

	public async Task<T> AddAsync(T entity)
	{
	  return (await context.Set<T>().AddAsync(entity)).Entity;
	}

	public async Task<T> UpdateAsync(T entity) =>
	  await Task.Run(() => context.Set<T>().Update(entity).Entity);

	public async Task<T> RemoveAsync(T entity) =>
	  await Task.Run(() => context.Set<T>().Remove(entity).Entity);

	public async Task<int> SaveChangesAsync()
	{
      try
	  {
		return await context.SaveChangesAsync();
	  }
	  catch
	  {
		context.Database.CurrentTransaction?.Rollback();
		throw;
	  }
	}

  }
}
