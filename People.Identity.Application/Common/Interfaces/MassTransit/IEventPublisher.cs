using People.Shared.AMQP.Events;

namespace People.Identity.Application.Common.Interfaces.MassTransit;

public interface IEventPublisher
{
  Task PublishUser(UserCreatedEvent e);
  Task PublishUser(UserUpdatedEvent e);
  Task PublishUser(UserDeletedEvent e);
  Task PublishApiKey(ApiKeyCreatedEvent e);
  Task PublishApiKey(ApiKeyDeletedEvent e);
}