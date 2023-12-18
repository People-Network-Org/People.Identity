using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.ApiKeyAggregate;
using People.Identity.Domain.ApiKeyAggregate.ValueObjects;

namespace People.Identity.Infrastructure.Persistance.Repositories;

public class ApiKeyRepository : Repository<ApiKey>, IApiKeyRepository
{
  public ApiKeyRepository(IdentityDbContext dbContext) : base(dbContext)
  {
  }

  public ApiKey? GetById(ApiKeyId id)
  {
    return _dbContext.ApiKeys.FirstOrDefault(k => k.Id == id);
  }

  public ApiKey? GetByKey(string key)
  {
    return _dbContext.ApiKeys.FirstOrDefault(k => k.Key == key);
  }
}