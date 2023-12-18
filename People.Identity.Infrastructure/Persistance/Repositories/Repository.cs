using People.Identity.Application.Common.Interfaces.Persistance;

namespace People.Identity.Infrastructure.Persistance.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
  protected readonly IdentityDbContext _dbContext;

  public Repository(IdentityDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public virtual void Add(T entity)
  {
    _dbContext.Add(entity);
    _dbContext.SaveChanges();
  }

  public virtual void AddRange(IEnumerable<T> entities)
  {
    _dbContext.AddRange(entities);
    _dbContext.SaveChanges();
  }

  public virtual void Delete(T entity)
  {
    _dbContext.Remove(entity);
    _dbContext.SaveChanges();
  }

  public virtual void DeleteRange(IEnumerable<T> entities)
  {
    _dbContext.RemoveRange(entities);
    _dbContext.SaveChanges();
  }

  public virtual void Update(T entity)
  {
    _dbContext.Update(entity);
    _dbContext.SaveChanges();
  }

  public virtual void UpdateRange(IEnumerable<T> entities)
  {
    _dbContext.UpdateRange(entities);
    _dbContext.SaveChanges();
  }
}