using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.ApiKeyAggregate;
using People.Identity.Domain.ApiKeyAggregate.ValueObjects;

namespace People.Identity.Infrastructure.Persistance.Repositories;

public class ApiKeyRepository : IApiKeyRepository
{
  private readonly IdentityDbContext _dbContext;

  public ApiKeyRepository(IdentityDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public void Add(ApiKey apiKey)
  {
    _dbContext.Add(apiKey);
    _dbContext.SaveChanges();
  }

  public void Delete(ApiKey apiKey)
  {
    _dbContext.Remove(apiKey);
    _dbContext.SaveChanges();
  }

  public ApiKey? GetById(ApiKeyId id)
  {
    return _dbContext.ApiKeys.FirstOrDefault(k => k.Id == id);
  }

  public ApiKey? GetByKey(string key)
  {
    return _dbContext.ApiKeys.FirstOrDefault(k => k.Key == key);
  }

  public void Update(ApiKey apiKey)
  {
    _dbContext.Update(apiKey);
    _dbContext.SaveChanges();
  }
}