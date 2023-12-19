using ErrorOr;

using MediatR;

using People.Identity.Application.Authentication.Common;
using People.Identity.Application.Common.Interfaces.Authentication;
using People.Identity.Application.Common.Interfaces.Persistance;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;

namespace People.Identity.Application.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ErrorOr<AuthenticationResult>>
{
  private readonly IUserRepository _userRepository;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;

  public ConfirmEmailCommandHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator)
  {
    _userRepository = userRepository;
    _jwtTokenGenerator = jwtTokenGenerator;
  }

  public async Task<ErrorOr<AuthenticationResult>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (_userRepository.GetByEmailCode(request.EmailCode) is not User user
      || user.EmailCode!.ExpiredDateTime < DateTime.UtcNow)
    {
      return Errors.User.UserNotFound;
    }

    user.ConfirmEmail(request.Password);

    var refreshToken = RefreshToken.Create(null, null);
    user.AddRefreshToken(refreshToken);

    _userRepository.Update(user);

    var token = _jwtTokenGenerator.GenerateToken(user);
    return new AuthenticationResult(user, token, refreshToken.Id.Value);
  }
}