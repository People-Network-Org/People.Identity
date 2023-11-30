using ErrorOr;

using MapsterMapper;

using MediatR;

using People.Identity.Application.Authentication.Common;
using People.Identity.Application.Common.Interfaces.MassTransit;
using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.ApiKeyAggregate;
using People.Identity.Domain.Common.Errors;
using People.Shared.AMQP.Events;

namespace People.Identity.Application.Authentication.Commands.DeleteKey;

public class DeleteKeyCommandHandler : IRequestHandler<DeleteKeyCommand, ErrorOr<ApiKeyResult>>
{
  private readonly IEventPublisher _eventPublisher;
  private readonly IApiKeyRepository _apiKeyRepository;
  private readonly IMapper _mapper;

  public DeleteKeyCommandHandler(
    IApiKeyRepository apiKeyRepository,
    IEventPublisher eventPublisher,
    IMapper mapper)
  {
    _apiKeyRepository = apiKeyRepository;
    _eventPublisher = eventPublisher;
    _mapper = mapper;
  }

  public async Task<ErrorOr<ApiKeyResult>> Handle(DeleteKeyCommand request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (_apiKeyRepository.GetByKey(request.Key) is not ApiKey apiKey)
      return Errors.Authentication.InvalidApiKey;

    var @event = _mapper.Map<ApiKeyDeletedEvent>(apiKey);
    await _eventPublisher.PublishApiKey(@event);

    _apiKeyRepository.Delete(apiKey);
    return new ApiKeyResult(apiKey);
  }
}
