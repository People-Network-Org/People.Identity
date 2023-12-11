using MassTransit;

using People.Identity.Application.Common.Interfaces.MassTransit;
using People.Shared.AMQP.Events;

namespace People.Identity.Infrastructure.MassTransit;

public class EventPublisher : IEventPublisher
{
  private readonly IPublishEndpoint _publishEndpoint;

  public EventPublisher(IPublishEndpoint publishEndpoint)
  {
    _publishEndpoint = publishEndpoint;
  }

  public async Task PublishUser(UserCreatedEvent e)
  {
    await _publishEndpoint.Publish(e);
  }

  public async Task PublishUser(UserUpdatedEvent e)
  {
    await _publishEndpoint.Publish(e);
  }

  public async Task PublishUser(UserDeletedEvent e)
  {
    await _publishEndpoint.Publish(e);
  }

  public async Task PublishApiKey(ApiKeyCreatedEvent e)
  {
    await _publishEndpoint.Publish(e);
  }

  public async Task PublishApiKey(ApiKeyDeletedEvent e)
  {
    await _publishEndpoint.Publish(e);
  }

  public async Task PublishUser(UserConfirmedEvent e)
  {
    await _publishEndpoint.Publish(e);
  }
}