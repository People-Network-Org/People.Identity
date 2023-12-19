using ErrorOr;

using MediatR;

using People.Identity.Application.Common.Interfaces.MassTransit;
using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Application.UserMediatR.Common;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;
using People.Shared.AMQP.Events;

namespace People.Identity.Application.UserMediatR.Commands.Delete;

public class DeleteHandler : IRequestHandler<DeleteCommand, ErrorOr<UserResult>>
{
  private readonly IUserRepository _userRepository;
  private readonly IEventPublisher _eventPublisher;

  public DeleteHandler(IUserRepository userRepository, IEventPublisher eventPublisher)
  {
    _userRepository = userRepository;
    _eventPublisher = eventPublisher;
  }

  public async Task<ErrorOr<UserResult>> Handle(DeleteCommand request, CancellationToken cancellationToken)
  {
    if (_userRepository.GetById(request.UserId) is not User user)
      return Errors.User.UserNotFound;

    _userRepository.Delete(user);
    var @event = new UserDeletedEvent(user.Id.Value);
    await _eventPublisher.PublishUser(@event);

    return new UserResult(user);
  }
}
