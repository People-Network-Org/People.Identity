using People.Identity.Domain.ApiKeyAggregate;
using People.Identity.Domain.ApiKeyAggregate.ValueObjects;

namespace People.Identity.Application.Common.Interfaces.Persistance;

public interface IApiKeyRepository : IRepository<ApiKey>
{
  ApiKey? GetById(ApiKeyId id);
  ApiKey? GetByKey(string key);
}