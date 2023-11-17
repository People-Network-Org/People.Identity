using People.Identity.Application.Common.Interfaces.Persistance;
using People.Shared.Auth.ApiKey.Interfaces;

namespace People.Identity.Infrastructure.Auth;

public class ApiKeyValidator : IApiKeyValidator
{
  private readonly IApiKeyRepository _apiKeyRepository;

  public ApiKeyValidator(IApiKeyRepository apiKeyRepository)
  {
    _apiKeyRepository = apiKeyRepository;
  }

  public bool IsApiKeyValid(string key)
  {
    return _apiKeyRepository.GetByKey(key) is not null;
  }
}