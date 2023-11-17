using ErrorOr;

using MediatR;

using People.Identity.Application.Authentication.Common;
using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.ApiKeyAggregate;

namespace People.Identity.Application.Authentication.Commands.CreateKey;

public class CreateKeyCommandHandler : IRequestHandler<CreateKeyCommand, ErrorOr<ApiKeyResult>>
{
  private readonly IApiKeyRepository _apiKeyRepository;

  public CreateKeyCommandHandler(IApiKeyRepository apiKeyRepository)
  {
    _apiKeyRepository = apiKeyRepository;
  }

  public async Task<ErrorOr<ApiKeyResult>> Handle(CreateKeyCommand request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    var apiKey = ApiKey.Create();
    _apiKeyRepository.Add(apiKey);

    return new ApiKeyResult(apiKey);
  }
}