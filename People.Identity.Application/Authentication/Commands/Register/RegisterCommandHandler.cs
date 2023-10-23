using ErrorOr;

using MediatR;

using People.Identity.Application.Authentication.Common;
using People.Identity.Application.Common.Interfaces.Authentication;
using People.Identity.Application.Common.Interfaces.Persistence;
using People.Identity.Domain.Common.Errors;
using People.Identity.Domain.UserAggregate;
using People.Identity.Domain.UserAggregate.Entities;

namespace People.Identity.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IUserRepository _userRepository;

  public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
  {
    _userRepository = userRepository;
    _jwtTokenGenerator = jwtTokenGenerator;
  }

  public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
  {
    await Task.CompletedTask;

    if (_userRepository.GetUserByEmail(command.Email) is not null)
      return Errors.User.DuplicateEmail;

    var user = User.Create(
      command.FirstName,
      command.LastName,
      command.Email,
      command.Email,
      null,
      command.Password);

    _userRepository.Add(user);

    var refreshToken = RefreshToken.Create(null, null);
    user.AddRefreshToken(refreshToken);
    _userRepository.Update(user);

    var token = _jwtTokenGenerator.GenerateToken(user);
    return new AuthenticationResult(user, token, refreshToken.Id.Value);
  }
}