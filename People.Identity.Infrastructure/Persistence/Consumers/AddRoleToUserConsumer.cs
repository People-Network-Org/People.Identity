using MassTransit;

using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;
using People.Identity.Domain.UserAggregate.ValueObjects;
using People.Shared.AMQP.Tasks;

namespace People.Identity.Infrastructure.Persistence.Consumers;

public class AddRoleToUserConsumer : IConsumer<AddRoleToUser>
{
  private readonly IUserRepository _userRepository;

  public AddRoleToUserConsumer(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task Consume(ConsumeContext<AddRoleToUser> context)
  {
    await Task.CompletedTask;

    if (_userRepository.GetById(UserId.Create(context.Message.Id)) is not User user)
    {
      return;
    }
    if (user.Roles.Any(r => r.NormalizedName == context.Message.Role.ToUpper()))
    {
      return;
    }

    var role = UserRole.Create(context.Message.Role);
    user.AddRole(role);

    _userRepository.Update(user);
  }
}