using ErrorOr;

using MediatR;

using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Application.UserMediatR.Common;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;

namespace People.Identity.Application.UserMediatR.Commands.AddRole;

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, ErrorOr<UserResult>>
{
  private readonly IUserRepository _userRepository;

  public AddRoleCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<UserResult>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (UserUtils.GetUserById(request.UserId, _userRepository) is not User user)
      return Errors.User.UserNotFound;

    if (UserUtils.GetUserRole(user, request.Role) is not null)
      return Errors.User.UserAlreadyHasRole;

    var role = UserRole.Create(request.Role);
    user.AddRole(role);

    _userRepository.Update(user);
    return new UserResult(user);
  }
}