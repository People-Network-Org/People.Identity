using Mapster;

using People.Identity.Domain.UserAggregate;
using People.Shared.AMQP.Events;

namespace People.Identity.Application.Common.Mapping;

public class UserAppMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<User, UserCreatedEvent>()
      .Map(dest => dest.Confirmed, src => false)
      .Map(dest => dest.Guid, src => Guid.Parse(src.Id.Value.ToString()));

    config.NewConfig<User, UserConfirmedEvent>()
      .Map(dest => dest.Guid, src => Guid.Parse(src.Id.Value.ToString()));
  }
}
