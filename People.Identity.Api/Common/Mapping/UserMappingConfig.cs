using Mapster;

using People.Identity.Application.UserMediatR.Common;
using People.Identity.Contracts.User;

namespace People.Identity.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<UserResult, UserResponse>()
      .Map(dest => dest.Id, src => Guid.Parse(src.User.Id.Value.ToString()))
      .Map(dest => dest, src => src.User);
  }
}