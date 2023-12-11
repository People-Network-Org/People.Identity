using MapsterMapper;

using MediatR;

using People.Identity.Application.Common.Interfaces.MassTransit;
using People.Identity.Domain.UserAggregate.Events;
using People.Shared.AMQP.Events;

namespace People.Identity.Application.Authentication.Events;

public class UserConfirmedHandler : INotificationHandler<UserConfirmed>
{
  private readonly IEventPublisher _eventPublisher;
  private readonly IMapper _mapper;

  public UserConfirmedHandler(IEventPublisher eventPublisher, IMapper mapper)
  {
    _eventPublisher = eventPublisher;
    _mapper = mapper;
  }

  public async Task Handle(UserConfirmed notification, CancellationToken cancellationToken)
  {
    var @event = _mapper.Map<UserConfirmedEvent>(notification.User);
    await _eventPublisher.PublishUser(@event);
  }
}