using Mapster;

using People.Identity.Domain.UserAggregate;
using People.Shared.AMQP.Events;

namespace People.Identity.Application.Common.Mapping;

public class UserMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<User, UserCreatedEvent>();
  }
}
