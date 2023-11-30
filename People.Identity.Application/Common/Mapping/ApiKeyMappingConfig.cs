using Mapster;

using People.Identity.Domain.ApiKeyAggregate;
using People.Shared.AMQP.Events;

namespace People.Identity.Application.Common.Mapping;

public class ApiKeyMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<ApiKey, ApiKeyCreatedEvent>();
    config.NewConfig<ApiKey, ApiKeyDeletedEvent>();
  }
}
