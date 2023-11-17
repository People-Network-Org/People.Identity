using Mapster;

using People.Identity.Application.Authentication.Commands.ConfirmEmail;
using People.Identity.Application.Authentication.Commands.DeleteKey;
using People.Identity.Application.Authentication.Commands.Register;
using People.Identity.Application.Authentication.Common;
using People.Identity.Application.Authentication.Queries.Login;
using People.Identity.Contracts.Authentication;
using People.Identity.Contracts.User;

namespace People.Identity.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<RegisterRequest, RegisterCommand>();

    config.NewConfig<RegisterResult, UserResponse>()
      .Map(dest => dest.Id, src => Guid.Parse(src.User.Id.Value.ToString()))
      .Map(dest => dest, src => src.User);

    config.NewConfig<ConfirmEmailRequest, ConfirmEmailCommand>();

    config.NewConfig<LoginRequest, LoginQuery>();

    config.NewConfig<AuthenticationResult, AuthenticationResponse>()
      .Map(dest => dest.Token, src => src.Token)
      .Map(dest => dest.RefreshToken, src => src.RefreshToken)
      .Map(dest => dest.Id, src => Guid.Parse(src.User.Id.Value.ToString()))
      .Map(dest => dest, src => src.User);

    config.NewConfig<DeleteApiKeyRequest, DeleteKeyCommand>();

    config.NewConfig<ApiKeyResult, ApiKeyResponse>()
      .Map(dest => dest.Key, src => src.ApiKey.Key);
  }
}