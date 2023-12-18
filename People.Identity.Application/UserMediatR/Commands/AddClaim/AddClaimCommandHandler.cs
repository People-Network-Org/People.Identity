using ErrorOr;

using MediatR;

using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Application.UserMediatR.Common;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;

namespace People.Identity.Application.UserMediatR.Commands.AddClaim;

public class AddClaimCommandHandler : IRequestHandler<AddClaimCommand, ErrorOr<UserResult>>
{
  private readonly IUserRepository _userRepository;

  public AddClaimCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<UserResult>> Handle(AddClaimCommand request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (UserUtils.GetUserById(request.UserId, _userRepository) is not User user)
      return Errors.User.UserNotFound;

    if (UserUtils.GetUserClaim(user, request.Type) is UserClaim userClaim)
    {
      user.RemoveClaim(userClaim);
    }

    var claim = UserClaim.Create(request.Type, request.Value);
    user.AddClaim(claim);

    _userRepository.Update(user);
    return new UserResult(user);
  }
}