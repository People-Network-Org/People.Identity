using MassTransit;

using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;
using People.Identity.Domain.UserAggregate.ValueObjects;
using People.Shared.AMQP.Tasks;

namespace People.Identity.Infrastructure.Persistence.Consumers;

public class AddClaimToUserConsumer : IConsumer<AddClaimToUser>
{
  private readonly IUserRepository _userRepository;

  public AddClaimToUserConsumer(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task Consume(ConsumeContext<AddClaimToUser> context)
  {
    await Task.CompletedTask;

    if (_userRepository.GetById(UserId.Create(context.Message.Id)) is not User user)
    {
      return;
    }
    if (user.Claims.Any(c =>
      c.Type == context.Message.Type &&
      c.Value == context.Message.Value))
    {
      return;
    }

    var claim = UserClaim.Create(context.Message.Type, context.Message.Value);
    user.AddClaim(claim);

    _userRepository.Update(user);
  }
}