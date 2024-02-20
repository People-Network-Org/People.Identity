using Mapster;

using People.Identity.Application.Authentication.Commands.Register;
using People.Identity.Application.UserMediatR.Commands.AddClaim;
using People.Identity.Application.UserMediatR.Commands.AddRole;
using People.Identity.Application.UserMediatR.Commands.Delete;
using People.Identity.Application.UserMediatR.Commands.RemoveClaim;
using People.Identity.Application.UserMediatR.Commands.RemoveRole;
using People.Shared.AMQP.Tasks;

namespace People.Identity.Infrastructure.Common.Mapping;

public class UserMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<AddClaimToUser, AddClaimCommand>()
      .Map(dest => dest.UserId, src => src.Id);

    config.NewConfig<RemoveClaimFromUser, RemoveClaimCommand>()
      .Map(dest => dest.UserId, src => src.Id);

    config.NewConfig<AddRoleToUser, AddRoleCommand>()
      .Map(dest => dest.UserId, src => src.Id);

    config.NewConfig<RemoveRoleFromUser, RemoveRoleCommand>()
      .Map(dest => dest.UserId, src => src.Id);

    config.NewConfig<CreateUser, RegisterCommand>();

    config.NewConfig<DeleteUser, DeleteCommand>()
      .Map(dest => dest.UserId, src => src.Guid);
  }
}