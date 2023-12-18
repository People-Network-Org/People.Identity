using Mapster;

using People.Identity.Application.UserMediatR.Common;
using People.Identity.Application.UserMediatR.Queries.Collection;
using People.Identity.Contracts.Common;
using People.Identity.Contracts.User;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.ValueObjects;
using People.Shared.AMQP.Events;

namespace People.Identity.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<UserResult, UserResponse>()
      .Map(dest => dest.Id, src => Guid.Parse(src.User.Id.Value.ToString()))
      .Map(dest => dest, src => src.User);

    config.NewConfig<UserResult, UserAdminResponse>()
      .Map(dest => dest.Id, src => Guid.Parse(src.User.Id.Value.ToString()))
      .Map(dest => dest.IsConfirmed, src => src.User.IsEmailConfirmed)
      .Map(dest => dest.EmailCode, src => src.User.EmailCode! == null! ? null : src.User.EmailCode.Code)
      .Map(dest => dest, src => src.User);

    config.NewConfig<User, UserCreatedEvent>()
      .Map(dest => dest.Guid, src => Guid.Parse(src.Id.Value.ToString()));

    config.NewConfig<Guid, UserId>()
      .MapWith(src => UserId.Create(src));

    config.NewConfig<UserId, Guid>()
      .MapWith(src => src.Value);

    config.NewConfig<UserId?, Guid?>()
      .MapWith(src => src! == null! ? null : src.Value);

    config.NewConfig<string?, UserId?>()
      .MapWith(src => src! == null! ? null : UserId.Create(Guid.Parse(src)));

    config.NewConfig<Guid?, UserId?>()
      .MapWith(src => !src.HasValue ? null : UserId.Create(src.Value));

    config.NewConfig<string, UserId>()
      .MapWith(src => UserId.Create(Guid.Parse(src)));

    config.NewConfig<ICollection<Guid>, CollectionQuery>()
      .Map(dest => dest.UserIds, src => src);

    config.NewConfig<List<UserResult>, CollectionResponse<UserAdminResponse>>()
      .Map(dest => dest.Items, src => src);

    config.NewConfig<List<UserResult>, CollectionResponse<UserResponse>>()
      .Map(dest => dest.Items, src => src);
  }
}