using MassTransit;

using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;
using People.Identity.Domain.UserAggregate.ValueObjects;
using People.Shared.AMQP.Tasks;

namespace People.Identity.Infrastructure.Persistence.Consumers;

public class UserConsumer :
  IConsumer<AddClaimToUser>,
  IConsumer<RemoveClaimFromUser>,
  IConsumer<AddRoleToUser>,
  IConsumer<RemoveRoleFromUser>
{
  private readonly IUserRepository _userRepository;

  public UserConsumer(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task Consume(ConsumeContext<AddClaimToUser> context)
  {
    await Task.CompletedTask;

    if (GetUserById(context.Message.Id) is not User user)
    {
      return;
    }
    if (GetUserClaim(user, context.Message.Type, context.Message.Value) is not null)
    {
      return;
    }

    var claim = UserClaim.Create(context.Message.Type, context.Message.Value);
    user.AddClaim(claim);

    _userRepository.Update(user);
  }

  public async Task Consume(ConsumeContext<RemoveClaimFromUser> context)
  {
    await Task.CompletedTask;

    if (GetUserById(context.Message.Id) is not User user)
    {
      return;
    }

    if (GetUserClaim(user, context.Message.Type, context.Message.Value) is UserClaim claim)
    {
      user.RemoveClaim(claim);
    }

    _userRepository.Update(user);
  }

  private UserClaim? GetUserClaim(User user, string type, string value)
  {
    return user.Claims.FirstOrDefault(c =>
      c.Type == type &&
      c.Value == value);
  }

  public async Task Consume(ConsumeContext<AddRoleToUser> context)
  {
    await Task.CompletedTask;

    if (GetUserById(context.Message.Id) is not User user)
    {
      return;
    }

    if (GetUserRole(user, context.Message.Role) is not null)
    {
      return;
    }

    var role = UserRole.Create(context.Message.Role);
    user.AddRole(role);

    _userRepository.Update(user);
  }

  public async Task Consume(ConsumeContext<RemoveRoleFromUser> context)
  {
    await Task.CompletedTask;

    if (GetUserById(context.Message.Id) is not User user)
    {
      return;
    }

    if (GetUserRole(user, context.Message.Role) is UserRole role)
    {
      user.RemoveRole(role);
    }

    _userRepository.Update(user);
  }

  private UserRole? GetUserRole(User user, string role)
  {
    return user.Roles.FirstOrDefault(r => r.NormalizedName == role.ToUpper());
  }

  private User? GetUserById(Guid id)
  {
    return _userRepository.GetById(UserId.Create(id));
  }
}