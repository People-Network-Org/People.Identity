using ErrorOr;

using MediatR;

using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;

namespace People.Identity.Application.Authentication.Commands.Revoke;

public class RevokeCommandHandler : IRequestHandler<RevokeCommand, ErrorOr<bool>>
{
  private readonly IUserRepository _userRepository;

  public RevokeCommandHandler(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<ErrorOr<bool>> Handle(RevokeCommand request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (_userRepository.GetById(request.UserId) is not User user)
      return Errors.User.UserNotFound;

    if (user.RefreshTokens.FirstOrDefault(rt => rt.Id == request.RefreshToken) is not RefreshToken refreshToken)
      return Errors.User.NotValidRefreshToken;

    user.RemoveRefreshToken(refreshToken);
    _userRepository.Update(user);

    return true;
  }
}