using People.Identity.Domain.ApiKeyAggregate.ValueObjects;
using People.Identity.Domain.Common.Models;

namespace People.Identity.Domain.ApiKeyAggregate;

public class ApiKey : AggregateRoot<ApiKeyId, Guid>
{
  public string Key { get; private set; }

  public DateTime CreatedDateTime { get; private set; }

  private ApiKey(
    ApiKeyId apiKeyId,
    string key,
    DateTime createdDateTime) : base(apiKeyId)
  {
    Key = key;
    CreatedDateTime = createdDateTime;
  }

  public static ApiKey Create()
  {
    var key = Guid.NewGuid().ToString().Replace("-", string.Empty);
    var apiKey = new ApiKey(
      ApiKeyId.CreateUnique(),
      key,
      DateTime.UtcNow);
    return apiKey;
  }

#pragma warning disable CS8618
  private ApiKey()
  {
  }
#pragma warning restore CS8618
}