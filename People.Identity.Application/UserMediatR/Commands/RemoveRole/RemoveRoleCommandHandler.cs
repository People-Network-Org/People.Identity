using ErrorOr;

using MediatR;

using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Application.UserMediatR.Common;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;

namespace People.Identity.Application.UserMediatR.Commands.RemoveRole;

public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand, ErrorOr<UserResult>>
{
  private readonly IUserRepository _userRepository;

  public RemoveRoleCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<UserResult>> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (UserUtils.GetUserById(request.UserId, _userRepository) is not User user)
      return Errors.User.UserNotFound;

    if (UserUtils.GetUserRole(user, request.Role) is UserRole role)
      user.RemoveRole(role);

    _userRepository.Update(user);
    return new UserResult(user);
  }
}