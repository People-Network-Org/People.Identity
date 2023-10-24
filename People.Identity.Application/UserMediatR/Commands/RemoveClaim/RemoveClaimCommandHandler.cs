using ErrorOr;

using MediatR;

using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Application.UserMediatR.Common;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;

namespace People.Identity.Application.UserMediatR.Commands.RemoveClaim;

public class RemoveClaimCommandHandler : IRequestHandler<RemoveClaimCommand, ErrorOr<UserResult>>
{
  private readonly IUserRepository _userRepository;

  public RemoveClaimCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<UserResult>> Handle(RemoveClaimCommand request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (UserUtils.GetUserById(request.UserId, _userRepository) is not User user)
      return Errors.User.UserNotFound;

    if (UserUtils.GetUserClaim(user, request.Type, request.Value) is UserClaim claim)
    {
      user.RemoveClaim(claim);
    }

    _userRepository.Update(user);
    return new UserResult(user);
  }
}