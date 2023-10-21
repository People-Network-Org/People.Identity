using Mapster;

using People.Identity.Application.Authentication.Commands.Register;
using People.Identity.Application.Authentication.Common;
using People.Identity.Application.Authentication.Queries.Login;
using People.Identity.Contracts.Authentication;

namespace People.Identity.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<RegisterRequest, RegisterCommand>();

    config.NewConfig<LoginRequest, LoginQuery>();

    config.NewConfig<AuthenticationResult, AuthenticationResponse>()
      .Map(dest => dest.Token, src => src.Token)
      .Map(dest => dest.Id, src => src.User.Id.Value)
      .Map(dest => dest, src => src.User);
  }
}