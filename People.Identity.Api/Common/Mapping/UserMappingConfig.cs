using Mapster;

using People.Identity.Application.UserMediatR.Common;
using People.Identity.Contracts.User;
using People.Identity.Domain.UserAggregate;
using People.Shared.AMQP.Events;

namespace People.Identity.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<UserResult, UserResponse>()
      .Map(dest => dest.Id, src => Guid.Parse(src.User.Id.Value.ToString()))
      .Map(dest => dest, src => src.User);

    config.NewConfig<User, UserCreatedEvent>()
      .Map(dest => dest.Guid, src => Guid.Parse(src.Id.Value.ToString()));
  }
}