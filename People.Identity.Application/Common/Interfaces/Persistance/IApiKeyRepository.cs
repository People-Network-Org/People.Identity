using People.Identity.Domain.ApiKeyAggregate;
using People.Identity.Domain.ApiKeyAggregate.ValueObjects;

namespace People.Identity.Application.Common.Interfaces.Persistance;

public interface IApiKeyRepository
{
  ApiKey? GetById(ApiKeyId id);
  ApiKey? GetByKey(string key);
  void Add(ApiKey apiKey);
  void Update(ApiKey apiKey);
  void Delete(ApiKey apiKey);
}