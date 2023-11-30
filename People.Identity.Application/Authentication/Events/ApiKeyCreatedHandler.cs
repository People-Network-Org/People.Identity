using MapsterMapper;

using MediatR;

using People.Identity.Application.Common.Interfaces.MassTransit;
using People.Identity.Domain.ApiKeyAggregate.Events;
using People.Shared.AMQP.Events;

namespace People.Identity.Application.Authentication.Events;

public class ApiKeyCreatedHandler : INotificationHandler<ApiKeyCreated>
{
  private readonly IEventPublisher _eventPublisher;
  private readonly IMapper _mapper;

  public ApiKeyCreatedHandler(IEventPublisher eventPublisher, IMapper mapper)
  {
    _eventPublisher = eventPublisher;
    _mapper = mapper;
  }

  public async Task Handle(ApiKeyCreated notification, CancellationToken cancellationToken)
  {
    var @event = _mapper.Map<ApiKeyCreatedEvent>(notification.ApiKey);
    await _eventPublisher.PublishApiKey(@event);
  }
}