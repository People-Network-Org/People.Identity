using MassTransit;

using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;
using People.Identity.Domain.UserAggregate.ValueObjects;
using People.Shared.AMQP.Tasks;

namespace People.Identity.Infrastructure.Persistence.Consumers;

public class RemoveRoleFromUserConsumer : IConsumer<RemoveRoleFromUser>
{
  private readonly IUserRepository _userRepository;

  public RemoveRoleFromUserConsumer(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task Consume(ConsumeContext<RemoveRoleFromUser> context)
  {
    await Task.CompletedTask;

    if (_userRepository.GetById(UserId.Create(context.Message.Id)) is not User user)
    {
      return;
    }

    if (user.Roles.First(r => r.NormalizedName == context.Message.Role.ToUpper()) is UserRole role)
    {
      user.RemoveRole(role);
    }
  }
}