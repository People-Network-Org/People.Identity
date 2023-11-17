using ErrorOr;

using MediatR;

using People.Identity.Application.Authentication.Common;
using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.ApiKeyAggregate;
using People.Identity.Domain.Common.Errors;

namespace People.Identity.Application.Authentication.Commands.DeleteKey;

public class DeleteKeyCommandHandler : IRequestHandler<DeleteKeyCommand, ErrorOr<ApiKeyResult>>
{
  private readonly IApiKeyRepository _apiKeyRepository;

  public DeleteKeyCommandHandler(IApiKeyRepository apiKeyRepository)
  {
    _apiKeyRepository = apiKeyRepository;
  }

  public async Task<ErrorOr<ApiKeyResult>> Handle(DeleteKeyCommand request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (_apiKeyRepository.GetByKey(request.Key) is not ApiKey apiKey)
      return Errors.Authentication.InvalidApiKey;

    _apiKeyRepository.Delete(apiKey);
    return new ApiKeyResult(apiKey);
  }
}
