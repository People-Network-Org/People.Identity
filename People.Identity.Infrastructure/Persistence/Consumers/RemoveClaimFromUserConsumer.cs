using MassTransit;

using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;
using People.Identity.Domain.UserAggregate.ValueObjects;
using People.Shared.AMQP.Tasks;

namespace People.Identity.Infrastructure.Persistence.Consumers;

public class RemoveClaimFromUserConsumer : IConsumer<RemoveClaimFromUser>
{
  private readonly IUserRepository _userRepository;

  public RemoveClaimFromUserConsumer(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task Consume(ConsumeContext<RemoveClaimFromUser> context)
  {
    await Task.CompletedTask;

    if (_userRepository.GetById(UserId.Create(context.Message.Id)) is not User user)
    {
      return;
    }

    if (user.Claims.First(c =>
      c.Type == context.Message.Type &&
      c.Value == context.Message.Value) is UserClaim claim)
    {
      user.RemoveClaim(claim);
    }
  }
}