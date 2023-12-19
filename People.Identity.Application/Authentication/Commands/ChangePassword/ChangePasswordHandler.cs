using ErrorOr;

using MediatR;

using People.Identity.Application.Authentication.Common;
using People.Identity.Application.Common.Interfaces.Authentication;
using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;

namespace People.Identity.Application.Authentication.Commands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<AuthenticationResult>>
{
  private readonly IUserRepository _userRepository;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;

  public ChangePasswordHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator)
  {
    _userRepository = userRepository;
    _jwtTokenGenerator = jwtTokenGenerator;
  }

  public async Task<ErrorOr<AuthenticationResult>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (_userRepository.GetById(request.UserId) is not User user)
      return Errors.User.UserNotFound;

    user.ChangePassword(request.Password);

    var refreshToken = RefreshToken.Create(null, null);
    user.AddRefreshToken(refreshToken);

    _userRepository.Update(user);

    var token = _jwtTokenGenerator.GenerateToken(user);
    return new AuthenticationResult(user, token, refreshToken.Id.Value);
  }
}
